   M S B A M L   `   `   `  ���� D  @Project02, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Project02	Project02      Project02.Content.set   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     N JFirstFloor.ModernUI, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null0mui&http://firstfloorsoftware.com/ModernUI  '	Project02clr-namespace:Project02  .���    ContentRoot#��[  $��0q�  � RowDefinitions  �� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��27.684 ��� $��3.316 � � ColumnDefinitions �� $��70 ��� $��70 ��� $
��Auto �.+��� $��套利金额��$
��Left=�$��30��$	��Top=�$��70��$��1,0,0,0q�$��0��$��0������ *
Foreground+ �� RelativeSource �� *FindAncestor+ �� AncestorType   Project02.MainWindow"  �� -    ��Name$
 tlje��$��1��$
��Left=�$��21�� �� TextWrapping$
 Wrap=�$��100000��$	��Top=�$��70��$��0,2,0,0q��� $��佣金费率��$
��Left=�$��30��$	��Top=�$��70��$��1,0,0,0q�$��1��$��0������ *
Foreground+ �� *FindAncestor+"  �� -   $
 yjfl��$��1��$
��Left=�$��21��$
 Wrap=�$��0.001��$	��Top=�$��70��$��0,2,0,0q�$��1���� $��印花税率��$
��Left=�$��30��$	��Top=�$��70��$��0,1,0,0q�$��2��$��0������ *
Foreground+ �� *FindAncestor+"  �� -   $
 yhsl��$
��Left=�$��21��$
 Wrap=�$��0.001��$	��Top=�$��70��$��0,3,0,0q�$��2��$��1���� $��融资利息��$
��Left=�$��30��$	��Top=�$��70��$��0,1,0,0q�$��3��$��0������ *
Foreground+ �� *FindAncestor+"  �� -   $
 rzlx��$
��Left=�$��21��$
 Wrap=�$
��0.08��$	��Top=�$��70��$��0,3,0,0q�$��3��$��1���� $��融资天数��$
��Left=�$��30��$	��Top=�$��70��$��0,1,0,0q�$��4��$��0������ *
Foreground+ �� *FindAncestor+"  �� -   $
 rzts��$
��Left=�$��21��$
 Wrap=�$��0��$	��Top=�$��70��$��0,3,0,0q�$��4��$��1���� $��融券利息��$
��Left=�$��30��$	��Top=�$��70��$��0,1,0,0q�$��5��$��0������ *
Foreground+ �� *FindAncestor+"  �� -   $
 rqlx��$
��Left=�$��21��$
 Wrap=�$
��0.08��$	��Top=�$��70��$��0,3,0,0q�$��5��$��1���� $��融券天数��$
��Left=�$��30��$	��Top=�$��70��$��0,1,0,0q�$��6��$��0��$��2������ *
Foreground+ �� *FindAncestor+"  �� -   $
 rqts��$
��Left=�$��21��$
 Wrap=�$��0��$	��Top=�$��70��$��0,3,0,0q�$��6��$��1��