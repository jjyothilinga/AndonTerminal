   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  > :IAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:IASIAS    IAS.Settings   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    4sys&clr-namespace:System;assembly=mscorlib    6dg-http://schemas.microsoft.com/wpf/2008/toolkit  localclr-namespace:IAS    N� Title$  Settings�� N� 
Background
 � � � � 	Resources  �� 
TargetType �� Property �� Value��%�   '��       '�� T     �� " ��.���  � ��$ Right=���  � ��
 �������  � ��$ 20(��� " ��.���  � ��$ 20(�.��� -   $��settingsGrid�� � RowDefinitions �� �� �� �� $
��Auto �.+��� $	��Top=����� $��Marquee��
�������$��12(�.���� .+���  �� Orientation$ 
Horizontal=�$��5q�.+��� $�� Message Marquee:���� -   $��	tbMarquee��$	��500������  �� Path$ Marquee�	 �� Mode$	 TwoWay=��� $��5q�$ 
Horizontal=�.+��� $��Message Marquee Speed:���� -   $��tbMarqueeSpeed��$��50������ $ MarqueeSpeed�$	 TwoWay=��� $��5q�$ 
Horizontal=�$y�Visible=�.+��� $��Issue Marquee Speed:���� -   $��tbIssueMarqueeSpeed��$��50������ $ IssuemarqueeSpeed�$	 TwoWay=��� $��5q�$ 
Horizontal=�$y�Visible=�.+��� $#��Last lost time accident date:��+  %Microsoft.Windows.Controls.DatePicker  -   $��	dpltaDate��$��50��
   Text
 �� $ LTADate�$	 TwoWay=��� $��1������ $��Escalation Settings��
�������$��12(�.��� $��5q�$y�Visible=�$
��Auto�� � ColumnDefinitions �� $
��Auto ��� $
��Auto �.+�)  #Microsoft.Windows.Controls.DataGrid  -   $��escalationDurationTable��   AutoGenerateColumns .  $
��Left=�   SelectionMode$ Single=�   SelectionUnit$ FullRow=�
��� � �$��0q�   CanUserAddRows .     CanUserReorderColumns .     CanUserSortColumns .  ����  @  :Microsoft.Windows.Controls.Primitives.DataGridColumnHeader��%�   '        �� Header_Style1��"  .���  � ��$ 15(���  � ��$
 Bold"���  � ��$ Center=���  � ��$ 0q�   Columns 7  1Microsoft.Windows.Controls.DataGridTemplateColumn  /  )Microsoft.Windows.Controls.DataGridColumn   Header$ 
Escalation��   
IsReadOnly .    CanUserReorder .     CellTemplate �� .R��� $��15(����� *Name+  $ Duration(in Minutes)�� �� .R���  �� MaxLines$ 1�� �� 	MaxLength$ 8��$��20(����� *
Duration+$	 TwoWay=��� $��2��$
��Auto��$y�Hidden=����� $��Password��
�������$��12(�.���� $ 
Horizontal=�$��5q�$
��Auto��.+��� $�� Current Password:��G� -   $��tbOldPassword��$	��100�� G� PasswordChar$ *��$	��Top=� G� 	MaxLength$ 4��$��20(�$
��Auto���� $��New Password:��$��5,0,0,0q�G� -   $��tbnewPassword��$	��100��$ *��$	��Top=�$ 4��$��20(�$
��Auto��{�.  �� -	   $��btnDone��$��3��$��Right=�$��Bottom=�$
��DONE��$��20(�$��5q�