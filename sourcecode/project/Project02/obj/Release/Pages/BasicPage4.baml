   M S B A M L   `   `   `  ���� D  @Project02, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Project02	Project02       Project02.Pages.BasicPage4   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     N JFirstFloor.ModernUI, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null0mui&http://firstfloorsoftware.com/ModernUI  '	Project02clr-namespace:Project02  .���    ContentRoot#��[  $��0q�  � ColumnDefinitions  �� $��90 ��� $��10 ��� $	��120 ��� $��20 ��� $��90 ��� $��20 ��� $	��120 ��� �� $
��Auto ��� $
��auto � � RowDefinitions �� $��30 ��� $��10 ��� $��30 ��� $��10 ��� $��30 ��� $��10 ��� $��30 ��� $��10 ��� $��30 ��� $��10 ��� $��30 ��� $��10 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 �.+�� $��
76,-23,0,0q�$
��Left=�$	��Top=�$��7��.+��� �� -    ��Name$ Header��$��未匹配到下单软件��$��
-10,0,10,0q��� $��股票代码��$
��Left=�$��31��$	��Top=�$��90������ *
Foreground+ �� RelativeSource �� *FindAncestor+ �� AncestorType   Project02.MainWindow"  �� $��购买价格��$
��Left=�$��30��$��0,2,0,0q�$��2��$	��Top=�$��90��$��2������ *
Foreground+ �� *FindAncestor+"  �� $��购买数量��$
��Left=�$��30��$��4��$	��Top=�$��90������ *
Foreground+ �� *FindAncestor+"  �� $��交易方式��$
��Left=�$��30��$��5��$	��Top=�$��90��$��2��$��0,10,0,0q����� *
Foreground+ �� *FindAncestor+"  �� -    ��Name$ buycode��$��2��$
��Left=�$��30�� �� TextWrapping$
 Wrap=�$	��Top=�$��90��$��0,1,0,0q��� -   $ buyprice��$��2��$
��Left=�$��30��$��0,2,0,0q�$��2��$
 Wrap=�$	��Top=�$��90��$��2���� -   $ 	buyamount��$��2��$
��Left=�$��30��$��4��$
 Wrap=�$	��Top=�$��90��$��0,1,0,0q��� -    ��Name$ PPbutton��$��匹配软件��$
��Left=�$��30��$��8��$	��Top=�$��90��$��0,1,0,0q��� -   $ 
AutoButton��$��快速下单��$��2��$
��Left=�$��30��$��8��$	��Top=�$��90��$��0,1,0,0q��� -   $��	listview1��$��4��$
��Left=�$	��198��$��9��$	��Top=�$	��400��$��4�� �� View �� .P��� $��股票代码��	 �� Width$
	 auto��
 �� DisplayMemberBinding
 �� *Code+�� $��股票名称��$
	 auto��
 �� *Name+�� $��交易价格��$
	 auto��
 �� *
WatchPrice+�� $��购买数量��$
	 auto��
 �� *Amount+�� $��时间��$
	 auto��
 �� *Time+�� $��交易方式��$
	 auto��
 �� *State+�� -   $��SelectDealState��$��2��$
��Left=�$��30��$��6��$	��Top=�$	��120�� �� DisplayMemberPath$
 Name�� �� SelectedValuePath$ ID��$��3��$��0,1,0,0q�