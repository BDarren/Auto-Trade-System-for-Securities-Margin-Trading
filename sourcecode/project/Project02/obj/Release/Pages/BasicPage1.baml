   M S B A M L   `   `   `  ���� D  @Project02, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:AutoCompleteComboBox.ControlsAutoCompleteComboBox.Controls  %clr-namespace:Project02	Project02       Project02.Pages.BasicPage1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     N JFirstFloor.ModernUI, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null0mui&http://firstfloorsoftware.com/ModernUI  :controls+clr-namespace:AutoCompleteComboBox.Controls  '	Project02clr-namespace:Project02  .���    ContentRoot#��[  $��0q�  � ColumnDefinitions  �� $	��150 ��� $��10 ��� $��90 ��� $��20 ��� $��90 ��� $��20 ��� $	��120 ��� �� $
��Auto ��� $
��auto � � RowDefinitions �� $��30 ��� $��10 ��� $��30 ��� $��10 ��� $��30 ��� $��10 ��� $��30 ��� $��10 ��� $��30 ��� $��10 ��� $��30 ��� $��10 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 �.+�� $��
76,-23,0,0q�$
��Left=�$	��Top=�$��7��.+��� �� -    ��Name$ Header��$��未匹配到下单软件��$��
-10,0,10,0q��� -    ��Name$ SearchButton��$��查询��$��2��$
��Left=�$��30��$	��Top=�$��70���� -    ��Name$ 	listview1��$
��Left=�$	��281��$	��Top=�$	��210��$	��190�� J� RenderTransformOrigin$ 0.598,1.974&�$��3��$��3��$��13�� �� View �� .P���  �� Width$ 50�� �� DisplayMemberBinding �� *Name+�� $ 50�� �� *Price+�� $ 90�� �� *amount+�� -   	 ��Name$	 LabelOfTime��$�� ��$��2��$
��Left=�$��30��$	��Top=�$��90��$��0,1,0,0q�$��2������ *
Foreground+
 �� RelativeSource
 �� *FindAncestor+ �� AncestorType   Project02.MainWindow"  �� -   $	 	StokeName��$�� ��$
��Left=�$��30��$	��Top=�$��66��$��2������ *
Foreground+
 �� *FindAncestor+"  �� $��股票代码��$��4��$
��Left=�$��31��$	��Top=�$��90������ *
Foreground+
 �� *FindAncestor+"  �� $��购买价格��$��4��$
��Left=�$��30��$��0,1,0,0q�$��2��$	��Top=�$��90������ *
Foreground+
 �� *FindAncestor+"  �� $��购买数量��$��4��$
��Left=�$��30��$��4��$	��Top=�$��90������ *
Foreground+
 �� *FindAncestor+"  �� $��交易方式��$��4��$
��Left=�$��30��$��6��$	��Top=�$��90��$��2������ *
Foreground+
 �� *FindAncestor+"  �� -    ��Name$ buycode��$��6��$
��Left=�$��30�� �� TextWrapping$
 Wrap=�$	��Top=�$��90���� -   $ buyprice��$��6��$
��Left=�$��30��$��0,1,0,0q�$��2��$
 Wrap=�$	��Top=�$��90���� -   $ 	buyamount��$��6��$
��Left=�$��30��$��4��$
 Wrap=�$	��Top=�$��90���� -	   $ PPbutton��$��匹配软件��$��4��$
��Left=�$��30��$��8��$	��Top=�$��90���� -
   $ 
AutoButton��$��自动交易��$��6��$
��Left=�$��30��$��7��$	��Top=�$��90��$��0,10,0,0q�$��2���� -   $ Watching��$��监控交易��$��6��$
��Left=�$��30��$��10��$	��Top=�$��90���� -   $ 	StopWatch��$��停止监控��$��4��$
��Left=�$��30��$��10��$	��Top=�$��90���� -   $��	listview2��$��4��$��4��$
��Left=�$	��124��$��12��$��4��$	��Top=�$	��400�� �� .P��� $��股票代码��$
 auto�� �� *Code+�� $��股票名称��$
 auto�� �� *Name+�� $��监控价格��$
 auto�� �� *Price+�� $��购买数量��$
 auto�� �� *Amount+�� $��时间��$
 auto�� �� *Time+�� $��交易方式��$
 auto�� �� *State+�� -   $��SelectDealState��$��6��$
��Left=�$��30��$��6��$	��Top=�$	��120�� �� DisplayMemberPath$
 Name�� �� SelectedValuePath$ ID��8   2AutoCompleteComboBox.Controls.AutoCompleteComboBox  -     Name$ 	StokeCode��$
��Left=�$��30��$	��Top=�