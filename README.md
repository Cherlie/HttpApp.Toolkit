		#WP8.1 ������Ӧ�ÿ����п���ʹ�õ��Ĺ�����
		####################
		
		##1.HttpApp.Toolkit\AppHelpers
		 -BackgroundTaskHelper ע��BackgroundTasks dll��ʱ�򣬿��ܻ��õ��ĳ�ʼ��������
		 -DownloadImage ��̨����ͼƬ
		 -UIHelper ����WP8.1ϵͳ״̬��
		
		##2.HttpApp.Toolkit\Behaviors
		
		 -ItemClickCommand MVVMģʽ��ʹ�õ���GridView����ListView��ItemClick�¼�ת��ΪCommand
		   eg: xmlns:Behavior="using:HttpApp.Toolkit.Behaviors"
		         <GridView Behavior:ItemClickCommand.Command="{Binding ItemClickCommand}" />
		
		 -ProgressBehavior MVVMģʽ�£��ڶ�����ϵͳ״̬������ʾ...�Ľ���  
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
		-ScrollIntoViewBehavior ��������ǰ��ѡ
		eg:
		```XAML
		<GridView SelectedItem="{Binding SelectedItem, Mode=TwoWay}">
		    <Interactivity:Interaction.Behaviors>
		         <Behavior:ScrollIntoViewBehavior LastFocusedItem="{Binding SelectedItem}"/>
		    </Interactivity:Interaction.Behaviors>
		</GridView>
		```
		##3.HttpApp.Toolkit\DataRequest
		-ServiceBase �ṩget��post��������ʽ��ֱ��ת��Ϊʵ���࣬��Ҫjson.net����
		
		##4.HttpApp.Toolkit\DataSource
		-DataSourceBase�������ػ�����
		ʹ�÷������̳�DataSourceBase<T>�ࣻ��д����LoadItemsAsync()�������ڻ�ȡ���ݣ���дAddItems(IList<T> items)���������ж������Ƿ��ظ������ظ�
			�����ӵ�������
		
		##5.HttpApp.Toolkit\TileHelper
		-TileCreator �����Զ������������
		ʹ�÷�����
		###1).�������������������ã�
		 ��������Ҫ�����û��ؼ�����XAML����������Ҫ��ʾ�ı��������ݣ�stile150x150f��stile150x150b��Щ���ǿؼ���Name������
		<Grid x:Name ="stile150x150f" Width="150" Height="150">����</Grid>�������������ݾ��Ǵ���150x150ʱ������Ҫ��ʾ�����ݣ�
		���Դ��������û��ؼ���ÿ������2��������Grid���ֱ��ʾ150x150�����棬310x150�����棬Ȼ���������û��ؼ������XML�ļ����ŵ�AssetsĿ¼�£���������ͨ��ע��һ��CXд�������̨����ʱ�������������
		```C#
		TileCreator tileCreator = new TileCreator(������������id��);
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
		###2).����������
		     ������������ƣ�ֻ�����޲εĹ��캯������
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
		-DefaultTimer ��ʱ��
		
		##7.HttpApp.Toolkit\Utilitys
		-CheckPixel �жϷֱ����Ƿ���1080p
		-DeviceInfo ��ȡwp8.1Ψһ��ʶ
		-Notifications ����֪ͨ
		-ObservableDictionary ���Ըı�֪ͨ�ֵ䣬�����֪��
		-TimestampUtils ʱ�����ϵͳʱ��֮��ת��
		-Util һЩ��֤



