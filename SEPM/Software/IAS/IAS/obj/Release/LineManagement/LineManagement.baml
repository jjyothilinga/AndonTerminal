   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  > :IAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:IASIAS    IAS.LineManagement   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    4sys&clr-namespace:System;assembly=mscorlib    6dg-http://schemas.microsoft.com/wpf/2008/toolkit  localclr-namespace:IAS    N� Title$  LineManagement�� N� 
Background
 � � �$
��Auto��$
��Auto�� N� ShowsNavigationUI .   � 	Resources )  #Microsoft.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value��%/   '        �� "  .���  � ��
 � � �.��� $
��Auto��$
��Auto�� � RowDefinitions �� $��* ��� $
��Auto �.+��� -   $��tbcLineControl��
��� � �.@��� $��LINES��.��A�  A� Columns$ 3��.+�   IAS.addModifyDeleteControl  -   	  Name$	 lineControl��   IAS.addModifyDeleteControl
   selectionChanged$"
 lineControl_selectionChanged��   
addClicked$ lineControl_addClicked��   deleteClicked$ lineControl_deleteClicked��$��0��$��5q�  -   $	 stationControl��$%
 stationControl_selectionChanged��$ stationControl_addClicked��$" stationControl_deleteClicked��$��1��$��0,5,5,5q�$
��Auto��$
��Auto��A�  A� Rows$ 2��.+�  -   $	 breakdownControl��$! breakdownControl_addClicked��$$ breakdownControl_deleteClicked��$��0,5,5,5q�  -   $	 qualityControl��$ qualityControl_addClicked��$" qualityControl_deleteClicked��$��1��$��0,5,5,5q��� $��ISSUES��.���  � ColumnDefinitions �� $��* ��� $
��Auto �.+�  -   $��dgOpenIssuesGrid��   AutoGenerateColumns .    
IsReadOnly .    CanUserAddRows .     CanUserDeleteRows .  "   VerticalScrollBarVisibility$
 Auto=�$   HorizontalScrollBarVisibility$
 Auto=�$
��Left=���. $
��Auto=�$
��Auto=�$
��Auto��$
��Auto��$y�Visible=�$��0q�   CanUserSortColumns .  ���� �� $��1�� �� Orientation$ Vertical=�$��Center=�$	��Top=�.+��� -   $��btnClose��$��CLOSE��$��5q�$��20(��� -   $��btnCloseAll��$��	CLOSE ALL��$��5q�$��20(��� $��Reports��.��� 
��� � � �� $
��Auto ��� $
��Auto ��� .+��� $��0��$��Report Configuration��$	��Top=����� .R��� 
�������$��Configuration��.���  �� $
��Auto ��� $
��Auto �.+�A� $ 1��$��0��$��2q�$y�Hidden=�.+��� $��Report Type :��
��������� -	   $��cmbViewTypeSelector��$
��Left=� �� SelectedIndex$ -1��.@��� .��Issue Report�� .��Daily Summary�� $��1��$ 
Horizontal=�$��5q�.+��� -
   $��lblFrom��$��From:��$
��Left=�$��12(�$
��Auto��
�������+  %Microsoft.Windows.Controls.DatePicker  -   $��dpFrom��$��12(�$
��Auto��$	��Top=�$
��Left=�$
��Auto���� -   $��lblTo��$	��To:��$
��Left=�$��12(�$
��Auto��
�������  -   $
��dpTo��$��12(�$
��Auto��$	��Top=�$
��Left=��� -   $��btnGenerate��$��5,0,0,0q�$��Generate���� -   $��	btnExport��$��5,0,0,0q�$��Export��$��Right=�$	��Top=�$
��Auto��$
��Auto���� $��1��$��Report��$
��Auto��$
��Auto������ .R��� 
�������$��Report��.���� .��  -   $��dgReportGrid�� .  .  .   .  ��. $
��Auto=�$
��Auto=�$
��Left=�$
��Auto��$
��Auto��$y�Visible=����� �� -   $��1��$��Right=�$��Bottom=�$
��DONE��$��5q�$��20(�