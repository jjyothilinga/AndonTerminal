   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      > :IAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:IASIAS 5         IAS.Settings   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      6sys&clr-namespace:System;assembly=mscorlib     5      6dg-http://schemas.microsoft.com/wpf/2008/toolkit  5      localclr-namespace:IAS  5        N� Title$  Settings��5	       N� 
Background
 � � � � 	Resources 5       �� 
TargetType �� Property �� Value��%�   '��       '�� �     �� 5   
   " ��6   .�5      ��  � ��$ Right=�64   �� 5       � ��
 �����6+   �� 5       � ��$ 20(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$ 20(�6)   5      5      .��5      � -   5        $��settingsGrid��5   	    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5       $
��Auto �6   .+�5"   
   �� $	��Top=�6   ��5#      �� 5$      $��Marquee��6   
�������$��12(�6=   .��5&      �� .+�5'      ��  �� Orientation$ 
Horizontal=�6   $��5q�66   .+�5(      �� $�� Message Marquee:��6   �� 5)      -   5        $��	tbMarquee��5)      $	��500��6/   ��6>   ��  �� Path$ Marquee�	 �� Mode$	 TwoWay=�5*      �� 5+      $��5q�6   $ 
Horizontal=�6(   .+�5,      �� $��Message Marquee Speed:��6   �� 5-      -   5        $��tbMarqueeSpeed��5-      $��50��60   ��6>   �� $ MarqueeSpeed�$	 TwoWay=�5.      �� 5/      $��5q�6   $ 
Horizontal=�6(   $y�Visible=�6A   .+�50      �� $��Issue Marquee Speed:��6   �� 51      -   5        $��tbIssueMarqueeSpeed��51      $��50��69   ��6G   �� $ IssuemarqueeSpeed�$	 TwoWay=�53      �� 54      $��5q�6   $ 
Horizontal=�6(   $y�Visible=�6A   .+�55      �� $#��Last lost time accident date:��6   +  %Microsoft.Windows.Controls.DatePicker  56      -   5        $��	dpltaDate��56   $   $��50��65   
   Text
 6C   �� $ LTADate�$	 TwoWay=�58      5:      5<      �� 5=   
   $��1��6   ��5>      �� 5?      $��Escalation Settings��6   
�������$��12(�6I   .��5A   
   � $��5q�6   $y�Visible=�6   $
��Auto��60    � ColumnDefinitions 5B      �� 5C      $
��Auto �6#   �� 5D      $
��Auto �6#   .+�5F      )  #Microsoft.Windows.Controls.DataGrid  -   5        $��escalationDurationTable��5F   8      AutoGenerateColumns .  $
��Left=�5G   ;      SelectionMode$ Single=�5H   "      SelectionUnit$ FullRow=�69   
��� � �$��0q�5I   8      CanUserAddRows .     CanUserReorderColumns .     CanUserSortColumns .  ��5G   "   ��  5L      @  :Microsoft.Windows.Controls.Primitives.DataGridColumnHeader��%�   '        �� 5M      Header_Style1��"  63   .�5N      ��  � ��$ 15(�65   �� 5O       � ��$
 Bold"�67   �� 5P       � ��$ Center=�6@   �� 5Q       � ��$ 0q�6<   5S      5T         Columns 5U      7  1Microsoft.Windows.Controls.DataGridTemplateColumn  5V      /  )Microsoft.Windows.Controls.DataGridColumn   Header$ 
Escalation��60      
IsReadOnly .    CanUserReorder .     CellTemplate 5W      �� 5X      .R�5Y   "   �� $��15(�6E   ��6-   �� *Name+6E   5Z      5\        5]      $ Duration(in Minutes)��60    5^      �� 5_      .R�5`   "   ��  �� MaxLines$ 1��6*    �� 	MaxLength$ 8��67   $��20(�5b   6   ��5a   6   �� *
Duration+$	 TwoWay=�5b   6   5c      5e      5g      5i      5j      �� 5k   
   $��2��6   $
��Auto��6!   $y�Hidden=�6/   ��5l      �� 5m      $��Password��6   
�������$��12(�6>   .��5o      �� $ 
Horizontal=�6   $��5q�64   $
��Auto��6?   .+�5p      �� $�� Current Password:��6   G� 5q      -   5        $��tbOldPassword��5q   "   $	��100��67    G� PasswordChar$ *��6F   $	��Top=�5r   "    G� 	MaxLength$ 4��6:   $��20(�6H   $
��Auto��6V   5s   "   �� 5t      $��New Password:��6   $��5,0,0,0q�64   G� 5u      -   5        $��tbnewPassword��5u   "   $	��100��67   $ *��6F   $	��Top=�5v   "   $ 4��6:   $��20(�6H   $
��Auto��6V   {�.  5w   5   5z      5�      �� 5�   
   -	   5        $��btnDone��5�      $��3��6   $��Right=�6-   $��Bottom=�6I   $
��DONE��5�      $��20(�6    $��5q�6.   69   5�      5�      