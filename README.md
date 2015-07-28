		#WP8.1 互联网应用开发中可能使用到的工具类
		####################
		
		##1.HttpApp.Toolkit\AppHelpers
		 -BackgroundTaskHelper 注册BackgroundTasks dll库时候，可能会用到的初始化帮助类
		 -DownloadImage 后台下载图片
		 -UIHelper 隐藏WP8.1系统状态栏
		
		##2.HttpApp.Toolkit\Behaviors
		
		 -ItemClickCommand MVVM模式下使用到将GridView或者ListView的ItemClick事件转化为Command
		   eg: xmlns:Behavior="using:HttpApp.Toolkit.Behaviors"
		         <GridView Behavior:ItemClickCommand.Command="{Binding ItemClickCommand}" />
		
		 -ProgressBehavior MVVM模式下，在顶部的系统状态栏上显示...的进度  
		 eg:
		 ```XAML
		  <Page
		    ...
		    xmlns:i="using:Microsoft.Xaml.Interactivity">
		    <i:Interaction.Behaviors>
		        <local:StatusBarBehavior IsVisible="True" 
		                                 BackgroundColor="#FF0000"
		                                 ForegroundColor="Blue"/>
		    </i:Interaction.Behaviors>
		    <Grid>
		        <!-- Content -->
		    <Grid>
		</Page>
		```
		-ScrollIntoViewBehavior 滚动到当前所选
		eg:
		```XAML
		<GridView SelectedItem="{Binding SelectedItem, Mode=TwoWay}">
		    <Interactivity:Interaction.Behaviors>
		         <Behavior:ScrollIntoViewBehavior LastFocusedItem="{Binding SelectedItem}"/>
		    </Interactivity:Interaction.Behaviors>
		</GridView>
		```
		##3.HttpApp.Toolkit\DataRequest
		-ServiceBase 提供get和post两种请求方式，直接转换为实体类，需要json.net引用
		
		##4.HttpApp.Toolkit\DataSource
		-DataSourceBase增量加载基础类
		使用方法：继承DataSourceBase<T>类；重写父类LoadItemsAsync()方法用于获取数据，重写AddItems(IList<T> items)方法用于判断数据是否重复，不重复
			则可添加到集合中
		
		##5.HttpApp.Toolkit\TileHelper
		-TileCreator 创建自定义磁贴帮助类
		使用方法：
		###1).创建二级磁贴（不常用）
		 首先你需要创建用户控件，用XAML画出磁贴所要显示的背景和数据，stile150x150f，stile150x150b这些都是控件的Name，比如
		<Grid x:Name ="stile150x150f" Width="150" Height="150">内容</Grid>，这个里面的内容就是磁贴150x150时候正面要显示的内容；
		可以创建两个用户控件，每个包含2个这样的Grid，分别表示150x150正反面，310x150正反面，然后将这两个用户控件保存成XML文件，放到Assets目录下，这样可以通过注册一个CX写的组件后台，定时更新这个磁贴。
		```C#
		TileCreator tileCreator = new TileCreator(“二级磁贴的id”);
		KeyValuePair<string, TileTemplateType> gridf = new KeyValuePair<string, TileTemplateType>("stile150x150f", TileTemplateType.TileSquare150x150Image);
		KeyValuePair<string, TileTemplateType> gridb = new KeyValuePair<string, TileTemplateType>("stile150x150b", TileTemplateType.TileSquare150x150Image);
		KeyValuePair<string, TileTemplateType> gridwb = new KeyValuePair<string, TileTemplateType>("swtile310x150f", TileTemplateType.TileWide310x150Image);
		KeyValuePair<string, TileTemplateType> gridwf = new KeyValuePair<string, TileTemplateType>("swtile310x150b", TileTemplateType.TileWide310x150Image);
		tileCreator.Configure(gridf, sqTile);
		tileCreator.Configure(gridb, sqTile);
		tileCreator.Configure(gridwb, sqWideTile);
		tileCreator.Configure(gridwf, sqWideTile);
		tileCreator.PinSecondaryTile();
		```
		###2).创建主磁贴
		     与二级磁贴类似，只是用无参的构造函数声明
		```C#
		private void TileLoaded()
		{
		    TileCreator tileCreator = new TileCreator();
		    KeyValuePair<string, TileTemplateType> gridf = new KeyValuePair<string, TileTemplateType>("tile150x150f", TileTemplateType.TileSquare150x150Image);
		    KeyValuePair<string, TileTemplateType> gridb = new KeyValuePair<string, TileTemplateType>("tile150x150b", TileTemplateType.TileSquare150x150Image);
		    KeyValuePair<string, TileTemplateType> gridwb = new KeyValuePair<string, TileTemplateType>("tile310x150f", TileTemplateType.TileWide310x150Image);
		    KeyValuePair<string, TileTemplateType> gridwf = new KeyValuePair<string, TileTemplateType>("tile310x150b", TileTemplateType.TileWide310x150Image);
		    tileCreator.Configure(gridf, sqTile);
		    tileCreator.Configure(gridb, sqTile);
		    tileCreator.Configure(gridwb, wideTile);
		    tileCreator.Configure(gridwf, wideTile);
		    tileCreator.PinTile();
		}
		```
		##6.HttpApp.Toolkit\Timers
		-DefaultTimer 定时器
		
		##7.HttpApp.Toolkit\Utilitys
		-CheckPixel 判断分辨率是否是1080p
		-DeviceInfo 获取wp8.1唯一标识
		-Notifications 本地通知
		-ObservableDictionary 属性改变通知字典，这个都知道
		-TimestampUtils 时间戳和系统时间之间转换
		-Util 一些验证



