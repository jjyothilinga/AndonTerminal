   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      > :IAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:IASIAS 5         IAS.LineManagement   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      6sys&clr-namespace:System;assembly=mscorlib     5      6dg-http://schemas.microsoft.com/wpf/2008/toolkit  5      localclr-namespace:IAS  5        N� Title$  LineManagement��5	       N� 
Background
 � � �$
��Auto��5
      $
��Auto��6&    N� ShowsNavigationUI .   � 	Resources 5      )  #Microsoft.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value��%T   '        �� 5   
   "  6   .�5      ��  � ��
 � � �6+   5      5      .��5      � $
��Auto��6   $
��Auto��6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5        $��tbcLineControl��5      
��� � �.@�5      �� $��LINES��6   .��5      A�  A� Columns$ 3��6   .+�5         IAS.addModifyDeleteControl  -   5        	  Name$	 lineControl��5   ;      IAS.addModifyDeleteControl
   selectionChanged$"
 lineControl_selectionChanged��5   +      
addClicked$ lineControl_addClicked��5   +      deleteClicked$ lineControl_deleteClicked��5   +   $��0��5   +   $��5q�6P   5$        5'   
   -   5        $	 stationControl��5'   7   $%
 stationControl_selectionChanged��5(   +   $ stationControl_addClicked��5)   +   $" stationControl_deleteClicked��5+   +   $��1��5'   '   $��0,5,5,5q�6O   $
��Auto��5-   +   $
��Auto��68   5/      A� 51       A� Rows$ 2��6   .+�53        -   5        $	 breakdownControl��53   +   $! breakdownControl_addClicked��54   +   $$ breakdownControl_deleteClicked��56   +   $��0,5,5,5q�53   E   5:        5<      -   5        $	 qualityControl��5<   8   $ qualityControl_addClicked��5=   +   $" qualityControl_deleteClicked��5?   +   $��1��5<   +   $��0,5,5,5q�6P   5B      5D      5F      5G      �� 5H      $��ISSUES��6   .��5I      �  � ColumnDefinitions 5J      �� 5K      $��* �6+   �� 5L      $
��Auto �6+   .+�5O        -   5        $��dgOpenIssuesGrid��5O   "      AutoGenerateColumns .    
IsReadOnly .    CanUserAddRows .     CanUserDeleteRows .  "   VerticalScrollBarVisibility$
 Auto=�5P   _   $   HorizontalScrollBarVisibility$
 Auto=�6�   $
��Left=�5Q   F   ��. $
��Auto=�6�   $
��Auto=�5R   *   $
��Auto��6Z   $
��Auto��6h   $y�Visible=�6u   $��0q�5S   "      CanUserSortColumns .  ��5Q   .   �� 5T      �� 5U      $��1��6!    �� Orientation$ Vertical=�61   $��Center=�6H   $	��Top=�6e   .+�5V      �� -   5        $��btnClose��5V   !   $��CLOSE��61   $��5q�6A   $��20(�6d   �� 5W      -   5        $��btnCloseAll��5W   !   $��	CLOSE ALL��64   $��5q�6H   $��20(�6n   5Y      5[      5\      �� 5]      $��Reports��6   .��5^      � 
��� � � 5`      �� 5a      $
��Auto �6(   �� 5b      $
��Auto �6(   �� 5c      .+�5g      �� $��0��6   $��Report Configuration��6,   $	��Top=�6J   ��5h      �� 5i      .R�5j   "   �� 
�������$��Configuration��6?   5k      .��5m      �  5n      �� 5o   "   $
��Auto �60   �� 5p   "   $
��Auto �60   .+�5t      A� $ 1��6*   $��0��63   $��2q�6@   $y�Hidden=�6L   .+�5u   "   �� $��Report Type :��6(   
�������6@   �� 5v   "   -	   5        $��cmbViewTypeSelector��5v   +   $
��Left=�6F    �� SelectedIndex$ -1��6a   .@�5x   &   �� .��63   Issue Report6A   �� 5y   &   .��63   Daily Summary6B   5z   #   5{      �� 5|      $��1��6)   $ 
Horizontal=�66   $��5q�6O   .+�5}   "   �� -
   5        $��lblFrom��5}   (   $��From:��67   $
��Left=�6G   $��12(�6b   $
��Auto��6q   
�������6~   +  %Microsoft.Windows.Controls.DatePicker  5~   "   -   5        $��dpFrom��5~   0   $��12(�6>   $
��Auto��6L   $	��Top=�6Z   $
��Left=�5   (   $
��Auto��6C   �� 5�   "   -   5        $��lblTo��5�   (   $	��To:��65   $
��Left=�6C   $��12(�6^   $
��Auto��6l   
�������6y     5�   "   -   5        $
��dpTo��5�   0   $��12(�6<   $
��Auto��6J   $	��Top=�6X   $
��Left=�6p   �� 5�   "   -   5        $��btnGenerate��5�   )   $��5,0,0,0q�6=   $��Generate��6N   6a   �� 5�   "   -   5        $��	btnExport��5�   ;   $��5,0,0,0q�6)   $��Export��6L   $��Right=�5�      $	��Top=�5�   !   $
��Auto��69   $
��Auto��5�   !   5�      5�      5�      �� 5�      $��1��6   $��Report��6,   $
��Auto��6=   $
��Auto��6K   ��5�      �� 5�      .R�5�   "   �� 
�������$��Report��6?   5�      .��5�      �� .��5�        -   5        $��dgReportGrid��5�   *    .  .  .   .  ��. $
��Auto=�5�      $
��Auto=�5�      $
��Left=�5�   >   $
��Auto��5�   #   $
��Auto��61   $y�Visible=�6>   ��5�   &   �� 5�      5�      5�      5�      5�      5�      �� 5�   
   -   5        $��1��5�      $��Right=�6   $��Bottom=�6:   $
��DONE��6U   $��5q�6d   $��20(�5�   '   5�      5�      