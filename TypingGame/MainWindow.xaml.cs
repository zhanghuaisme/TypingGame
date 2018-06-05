/****************************
 * 项目名：指法练习游戏
 * 创建者：张华
 * 创建日：2010/03/28
 */

/* 变更历史
 */

using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace TypingGame
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 变量
        private DispatcherTimer tmMove = new DispatcherTimer();//控制移动间隔时间
        private DispatcherTimer tmGeneral = new DispatcherTimer();//控制产生字母间隔时间

        private Random randomNumber = new Random();//产生随机数
        private ArrayList charaters = new ArrayList();//字母数组
        private int level = 8;//字母下落速度
        private double generalTime = 1;//产生字母间隔时间（控制数量）
        private double moveScope = 0.5;//左右移动幅度
        private int counter = 0;//字母总数计数器
        private int score = 0;//打对的字母数
        private int maxNum = 100;//计数的最大数
        private int startLetter = 65;//出现字母的初始字母（ASCII）
        private int endLetter = 91;//出现字母的结束字母
        private Image ImgCoin = new Image();//空中爆炸图像
        private Image OnLandImage = new Image();//地面爆炸图像

        private PlayerController playerCtl = new PlayerController();

        private bool isRunning = false;//是否正在进行练习
        private bool isSilent = false;//是否静音

        private MessageController msgCtl = new MessageController();//消息控制器
        
        private Storyboard sbdDown = null;//幕玻璃板打开动画
        private Storyboard sbdUp = null;//幕玻璃板合上动画

        private Storyboard sbdRol = null;//用户名面板旋出动画
        private Storyboard sbVanishing = null;

        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion 

        #region 构造方法
        /// <summary>
        /// 构造方法，页面初始
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion 

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                logger.Debug("Window_Loaded Start");
                InitializeTimer();//初始化定时器

                InitializeUIElementValue();//页面元素初始化
                //初始化爆炸图像
                InitializeBom();

                this.SetStoryboard();
                logger.Debug("Window_Loaded End");
            }
            catch (Exception )
            {
                MessageBox.Show("页面加载异常", "异常", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 初始化爆炸图像
        /// </summary>
        private void InitializeBom()
        {
            BitmapImage bitmapImage = ImageManager.LoadBitMapImage("Image\\bom.ico");
            BitmapImage bitmapImageCoin = ImageManager.LoadBitMapImage("Image\\coin.ico");

            InitializeImage(ImgCoin, bitmapImageCoin);//初始化空中爆炸图像
            AddChildToCanvas(ImgCoin, this.CanvCoin);//将图片控件添加到画布
            this.CanvCoin.Visibility = Visibility.Hidden;//加分效果图层隐藏

            InitializeImage(OnLandImage, bitmapImage);//初始化地面爆炸图像
            AddChildToCanvas(OnLandImage, this.CanvasMain);//将图片控件添加到画布
            OnLandImage.Visibility = Visibility.Hidden;//默认隐藏
        }

        /// <summary>
        /// 页面元素值初始化
        /// </summary>
        private void InitializeUIElementValue()
        {
            this.TxtName.Text = Constant.DefaultTxtBoxName;//请输入练习者名字
            this.CmbLevel.SelectedIndex = 0;//设置下拉框初始选择第一个选项
            CmbLevelTopCav.SelectedIndex = 3;
            this.CmbLevel.SelectionChanged += CmbLevel_SelectionChanged;
            this.SldGeneral.Value = 0;//设置滑竿初始值
            this.MsgShow.Visibility = Visibility.Hidden;//消息隐藏
        }

        /// <summary>
        /// 初始化定时器
        /// </summary>
        private void InitializeTimer()
        {
            tmGeneral.Tick += new EventHandler(TmGeneral_Tick);//注册定时事件
            tmGeneral.Interval = TimeSpan.FromSeconds(generalTime);//移动间隔时间0.05秒

            tmMove.Tick += new EventHandler(TmMove_Tick);//注册定时事件
            tmMove.Interval = TimeSpan.FromSeconds(0.05);//移动间隔时间0.05秒
        }


        
        #endregion 

        #region 级别设置
        /// <summary>
        /// 级别设置
        /// </summary>
        private void SetLevel()
        {
            this.level = this.CmbLevel.SelectedIndex + 1;//设置级别为选择的项
            //设置产生字母间隔时间为：级别大则间隔时间短（0.1-1）
            this.generalTime = (double)(10 - this.CmbLevel.SelectedIndex) / 10;
        }
        #endregion 

        #region 定时器触发事件，字母产生和移动
        /// <summary>
        /// 产生字母
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmGeneral_Tick(object sender, EventArgs e)
        {
            try
            {
                tmGeneral.Interval = TimeSpan.FromSeconds(generalTime);//移动间隔时间
                Label lblCharacter = new Label();//生成装载字母的标签
                //设置字母
                lblCharacter.Content = (char)(randomNumber).Next(startLetter, endLetter);

                //设置大小
                lblCharacter.Height = 35;
                lblCharacter.Width = 30;
                lblCharacter.FontSize = 20;

                AddChildToCanvas(lblCharacter, this.CanvasMain);//将字母控件添加到画布
                charaters.Add(lblCharacter);//加入到字母数组
                //设置字母位置
                Canvas.SetTop(lblCharacter, 0);
                Canvas.SetLeft(lblCharacter, 
                    randomNumber.Next(0, (int)(this.CanvasMain.Width - lblCharacter.Width)));
                counter++;//计数增加1
                this.LblCount.Content = counter;//显示计数到窗口
                this.SldGeneral.Value = (double)counter / 100;//显示计数到进度

                if (counter >= maxNum)
                {//如果计数到了，停止产生字母
                    tmGeneral.Stop();
                }
            }
            catch (Exception )
            {
                MessageBox.Show("产生字母异常", "异常", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// 将控件添加到画布
        /// </summary>
        /// <param name="element">控件</param>
        private void AddChildToCanvas(UIElement element, Canvas canvas)
        {
            canvas.Children.Add(element);
        }

        /// <summary>
        /// 字母移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmMove_Tick(object sender, EventArgs e)
        {
            try
            {
                SetImageHidden(OnLandImage);//隐藏地面爆炸图片

                Label charater;//字母
                double yPosition;//字母的x坐标
                double xPosition;//字母的y坐标
                double speed;//字母移动速度
                //遍历字母数组，移动每一个字母
                for (int i = 0; i < charaters.Count; i++)
                {
                    charater = (Label)charaters[i];
                    yPosition = Canvas.GetTop(charater);
                    xPosition = Canvas.GetLeft(charater);
                    //速度=级别*随机数
                    speed = level * randomNumber.NextDouble();
                    //控制随机左右移动，0:左移，1：右移
                    if (randomNumber.Next(2) == 0)//2和0换成别的数可控制风向（可换成变量以备扩展）
                    {//左移
                        xPosition -= moveScope;
                    }
                    else
                    {//右移
                        xPosition += moveScope;
                    }
                    //下移
                    yPosition += 0.2 + speed;

                    //竖直方向移动位置
                    if (yPosition >= this.CanvasMain.Height - charater.Height)
                    {//掉到下面后
                        SetBomb(OnLandImage, charater);//爆炸
                        //yPosition = 0;
                        RemoveLabel(charater);//字母爆炸后消失
                        i--;//控制循环，移除一个字母后由于数组结构变了，需要重新遍历当前下标的字母
                        GetScore();//如果计数结束，最后一个字母掉到地面，开始计算成绩并设置消息
                        continue;//计数未结束，继续遍历下一个字母
                    }

                    //水平方向移动位置
                    if (xPosition > this.CanvasMain.Width - charater.Width)
                    {//从右面移出窗口
                        //控制不会从右侧移出屏幕
                        xPosition = this.CanvasMain.Width - charater.Width;
                    }
                    else if (xPosition < 0)
                    {//从左面移出窗口
                        xPosition = 0;//控制不会从左侧移出屏幕
                    }

                    //设置字母位置
                    Canvas.SetTop(charater, yPosition);
                    Canvas.SetLeft(charater, xPosition);
                }
            }
            catch (Exception )
            {
                MessageBox.Show("产生字母异常", "异常", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 如果计数结束则计算成绩并显示消息同时初始化程序
        /// </summary>
        private void GetScore()
        {
            if (counter >= maxNum && charaters.Count < 1)
            {
                //将按钮上的字设置成“开始”
                BtnStart.Content = (string)this.FindResource("KeyBtnStart");
                tmGeneral.Stop(); //暂停产生字母
                tmMove.Stop(); //移动暂停
                isRunning = false;//联系没有在进行
                ShowMessage();//显示消息
                SaveScore(score, level, this.TxtName.Text);
                SetImageHidden(OnLandImage);//隐藏地面爆炸图片
            }
        }
        /// <summary>
        /// 保存联系记录
        /// </summary>
        /// <param name="score">得分</param>
        /// <param name="level">级别</param>
        /// <param name="name">名字</param>
        private void SaveScore(int score, int level, string name)
        {
            RecordController recordCtl = new RecordController();
            recordCtl.SaveScore(score, level, name);
        }

        /// <summary>
        /// 显示消息
        /// </summary>
        private void ShowMessage()
        {
            msgCtl.SetMessage(this.MsgShow, level, score);//设置构造消息
            msgCtl.ShowMessage();//显示消息
            //设置消息显示位置
            Canvas.SetTop(this.MsgShow, 100);
            Canvas.SetLeft(this.MsgShow, (CanvasMain.Width - this.MsgShow.Width) / 2);
        }
        #endregion 

        #region 键盘按下
        /// <summary>
        /// 键盘按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key.Equals(Key.Space) && BtnStart.IsEnabled == true)
                {//如果是空格键则相应“开始/暂停”，快捷键功能
                    BtnStart_Click(null, null);
                    this.BtnStart.Focus();//开始按钮获取光标
                    e.Handled = true;//阻断消息传送，屏蔽系统默认的空格键应
                    return;
                }
                if (!isRunning)
                {
                    return;
                }

                Label lblChar = null;
                FrameworkElement element = null;
                int i = 0;
                for (i = 0; i < this.CanvasMain.Children.Count; i++)
                {//遍历画布上的每一个元素
                    element = (FrameworkElement)this.CanvasMain.Children[i];
                    if (element is Label)
                    {//如果是Lebel标签（如果是字母）
                        lblChar = (Label)element;//赋值给局部变量
                    }
                    else
                    {
                        continue;
                    }

                    //if (Keyboard.Modifiers == ModifierKeys.Shift)
                    //{
                    //    key = key.ToUpper();
                    //}

                    if (lblChar != null && e.Key.ToString().Equals(lblChar.Content.ToString()))
                    {//如果是字母，如果和按键字母相同
                        //SetBomb(dispImage, lblChar);//爆炸
                        SetCoin(this.CanvCoin, lblChar);//空中得分

                        score++;//成绩加1
                        this.TxtScore.Text = score.ToString();//显示成绩
                        RemoveLabel(lblChar);//字母消失
                        i--;//遍历当前位置新的字母
                        GetScore();//如果计数到，则计算成绩
                        break;//找到字母后则不再遍历剩余字母
                    }
                }
                if (i == this.CanvasMain.Children.Count)
                {//屏幕上没有按下的字母
                    PlaySound(Constant.SoundType.PressError);
                }
            }
            catch (Exception )
            {
                MessageBox.Show("键盘按下处理异常", "异常", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 将控件从画布移除
        /// </summary>
        /// <param name="element">控件</param>
        private void RemoveLabel(UIElement element)
        {
            this.CanvasMain.Children.Remove(element);
            charaters.Remove(element);//从数组移除
        }
        #endregion

        #region 开始/暂停 按钮按下
        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.SetLevel();//设置游戏级别
                if (counter >= maxNum && charaters.Count <= 0)
                {//如果计数结束则清零计数器和成绩
                    counter = 0;
                    score = 0;
                    this.TxtScore.Text = "0";
                }

                if (isRunning)
                {//设置暂停效果
                    BtnStart.Content = (string)this.FindResource("KeyBtnStart");
                    tmGeneral.Stop(); //产生暂停
                    tmMove.Stop(); //移动暂停
                    isRunning = false;
                }
                else
                {//设置开始效果
                    Start();
                }
            }
            catch (Exception )
            {
                MessageBox.Show("开始/暂停 按钮按下处理异常", "异常", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 设置开始效果
        /// </summary>
        private void Start()
        {
            BtnStart.Content = (string)this.FindResource("KeyBtnPause");
            if (counter < maxNum)
            {//如果计数不结束则产生，结束则不再开始
                tmGeneral.Start(); //产生开始
            }
            tmMove.Start(); //移动开始
            isRunning = true;
            msgCtl.HideMessage();//游戏开始则隐藏消息框
        }
        #endregion

        #region 重来按钮按下
        /// <summary>
        /// 重来游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReSet();
            }
            catch (Exception )
            {
                MessageBox.Show("重来按钮按下处理异常", "异常",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 重来按钮按下,游戏重新开始
        /// </summary>
        private void ReSet()
        {
            this.SetLevel();//设置游戏级别
            this.counter = 0;//清零计数
            this.score = 0;//清零成绩
            this.TxtScore.Text = this.score.ToString();//清零显示成绩
            //清空屏幕上的字母
            for (int i = 0; i < charaters.Count; i++)
            {
                RemoveLabel((Label)charaters[i]);
                //this.CanvasMain.Children.Remove((Label)charaters[i]);
                //charaters.Remove(charaters[i]);
                i--;
            }
            Start();//开始游戏
        }
        #endregion 

        #region 级别改变，重新开始游戏
        /// <summary>
        /// 级别改变，重新开始游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ReSet();
            }
            catch (Exception )
            {
                MessageBox.Show("级别改变处理异常", "异常",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion 

        #region Play按钮按下
        /// <summary>
        /// Play 按钮按下，游戏开始，玻璃板拉开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PlayOpenSound();
                sbdDown.Begin();//屏幕拉开动画
                GridElementAll.IsEnabled = true;//窗口控件可用

                if (!isRunning)
                {//如果处于暂停状态或者新游戏则自动开始
                    BtnStart_Click(null, null);
                }

                this.CmbLevel.SelectedIndex = CmbLevelTopCav.SelectedIndex;
            }
            catch (Exception )
            {
                MessageBox.Show("Play按钮按下处理异常", "异常",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 后退，屏幕玻璃板拉上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sbdUp.Begin();//屏幕玻璃板合上动画
                GridElementAll.IsEnabled = false;//窗口控件不可用
                if (isRunning)
                {//如果处于运行状态则暂停
                    BtnStart_Click(null, null);
                }

                SetShow();
            }
            catch (Exception )
            {
                MessageBox.Show("后退，屏幕玻璃板拉上处理异常", "异常",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 设置动画板
        /// </summary>
        private void SetStoryboard()
        {
            sbdDown = (Storyboard)this.FindResource("SbdDown");
            sbdUp = (Storyboard)this.FindResource("SbdUp");
            sbdRol = (Storyboard)this.FindResource("Revolving");
            sbVanishing = (Storyboard)this.FindResource("SbVanishing");
        }
        #endregion 

        #region 设置，排名按钮按下
        /// <summary>
        /// 排名按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSort_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PlayClickSound();
                SortShow();
            }
            catch (Exception )
            {
                MessageBox.Show("排名按钮按下处理异常", "异常",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 排行榜面板显示
        /// </summary>
        private void SortShow()
        {
            RecordController recordCtl = new RecordController();

            DataRow[] drs = recordCtl.GetRecord();
            this.LvScore.ItemsSource = drs;

            sbdRol.Begin();
            CanvSet.Visibility = Visibility.Collapsed;
            CanvSort.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// 设置按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PlayClickSound();
                SetShow();
            }
            catch (Exception )
            {
                MessageBox.Show("设置按钮按下处理异常", "异常",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 设置面板显示
        /// </summary>
        private void SetShow()
        {
            sbdRol.Begin();
            CanvSort.Visibility = Visibility.Collapsed;
            CanvSet.Visibility = Visibility.Visible;
        }
        #endregion 

        #region 姓名输入处理
        /// <summary>
        /// 姓名输入得到光标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtName_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Constant.DefaultTxtBoxName.Equals(this.TxtName.Text))
                {
                    this.TxtName.Text = string.Empty;
                    //设置字体样式，通常，黑色
                    this.TxtName.FontStyle = FontStyles.Normal;
                    this.TxtName.Foreground = System.Windows.Media.Brushes.Black;
                }
            }
            catch (Exception )
            {
                MessageBox.Show("姓名输入处理异常", "异常",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 姓名输入光标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtName_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.Empty.Equals(this.TxtName.Text))
                {
                    this.TxtName.Text = Constant.DefaultTxtBoxName;
                    //设置字体样式，斜体，灰色
                    this.TxtName.FontStyle = FontStyles.Italic;
                    this.TxtName.Foreground = System.Windows.Media.Brushes.Gray;
                }
            }
            catch (Exception )
            {
                MessageBox.Show("姓名输入光标离开处理异常", "异常",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion 

        #region 按钮音效
        /// <summary>
        /// 鼠标进入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                PlayEnterSound();
            }
            catch (Exception )
            {
                MessageBox.Show("鼠标进入事件处理异常", "异常",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 鼠标进入声音
        /// </summary>
        private void PlayEnterSound()
        {
            PlaySound(Constant.SoundType.ButtonEnter);
        }

        /// <summary>
        /// 播放单击声音
        /// </summary>
        private void PlayClickSound()
        {
            PlaySound(Constant.SoundType.ButtonClick);
        }

        /// <summary>
        /// 播放拉开声音
        /// </summary>
        private void PlayOpenSound()
        {
            PlaySound(Constant.SoundType.Open);
        }

        /// <summary>
        /// 播放声音
        /// </summary>
        /// <param name="type">声音类型</param>
        private void PlaySound(Constant.SoundType type)
        {
            if (!isSilent)
            {
                playerCtl.Play(type);//播放声音
            }
        }
        #endregion 

        #region 爆炸效果
        /// <summary>
        /// 显示爆炸效果
        /// </summary>
        /// <param name="lblChar">字母Label</param>
        private void SetBomb(Image image, Label lblChar)
        {
            SetImageVisible(image, lblChar);//显示图片
            PlaySound(Constant.SoundType.Bom);//播放声音
        }

        /// <summary>
        /// 金币得分效果（空中爆炸）
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="lblChar"></param>
        private void SetCoin(Canvas canvas, Label lblChar)
        {
            this.LblCoin.Content = this.LblCoin.Content.ToString().Substring(0, 1) + level;
            SetImageVisible(canvas, lblChar);//显示图片
            sbVanishing.Begin();
            PlaySound(Constant.SoundType.Coin);
        }


        /// <summary>
        /// 初始化爆炸图片
        /// </summary>
        /// <param name="image">图片控件</param>
        /// <param name="bitmapImage">图片</param>
        private void InitializeImage(Image image, BitmapImage bitmapImage)
        {
            image.Source = bitmapImage;//设置图片到控件
            image.Width = 32;//控件大小
            image.Height = 32;
        }


        /// <summary>
        /// 爆炸图片显示
        /// </summary>
        /// <param name="image">图片控件</param>
        /// <param name="charater">字母Label</param>
        private void SetImageVisible(UIElement element, Label charater)
        {
            element.Visibility = Visibility.Visible;//显示图片
            //设置图片位置为字母的位置
            Canvas.SetTop(element, Canvas.GetTop(charater));
            Canvas.SetLeft(element, Canvas.GetLeft(charater));
        }

        /// <summary>
        /// 爆炸图片隐藏
        /// </summary>
        /// <param name="image">图片控件</param>
        private void SetImageHidden(Image image)
        {
            image.Visibility = Visibility.Hidden;//设置隐藏
        }

        #endregion

        #region 静音设置
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CkbSilent_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.isSilent = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CkbSilent_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.isSilent = false;
        }

        #endregion 
    }
}
