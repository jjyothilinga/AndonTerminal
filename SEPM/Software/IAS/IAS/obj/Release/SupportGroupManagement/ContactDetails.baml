   M S B A M L   `   `   `  ���� >  :IAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:IASIAS      IAS.ContactDetails   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    6dg-http://schemas.microsoft.com/wpf/2008/toolkit  localclr-namespace:IAS  $
��Auto��$
��Auto��  � 	Resources   �� 
TargetType �� Property �� Value��%�   '��       '�� :     '�� �     �� " ��.���  � ��$	 300����  � ��$ 20(��� " ��.���  � ��$	 100����  � ��$ Right=���  � ��
 �������  � ��$ 20(��� " ��.���  � ��$ 30(�.��� 
��� � � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+�� $��0��$	��5,5q�$
��Auto�� � ColumnDefinitions �� $
��Auto ��� $
��Auto �.+��� $��0��$��SESA ID:��$
��Left=��� -   $
��tbID��$��1�� �� 
IsReadOnly .  ����  �� Path$ ID�� $��1��$	��5,5q�$
��Auto�� �� $
��Auto ��� $
��Auto �.+��� $��0��$��	Password:��$
��Left=�G� -   $��
tbPassword��$��1��� -   $��DeviceIdGrid��$��2��$	��5,5q�$
��Auto�� �� $
��Auto ��� $
��Auto �.+��� $��0��$�� CONTACT NAME:��$
��Left=��� -   $��tbContactName��$��1�� .  ���� $
 Name�� $��3��$	��5,5q�$
��Auto�� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$�� CONTACT NUMBER:��$
��Left=��� -   $��tbContactNumber��$��1�� .  ���� $ Number��� -   $��cbProcurement��$��2��{�. $��Center=�$��
10,10,0,10q�$y�Hidden=� d� 	IsChecked �� $ IsProcurement�	 �� Mode$	 TwoWay=��� $��3��$��Procurement��$
��Left=�$y�Hidden=�A� 
 A� Columns$
 4��$��4��.+��� -   $��lineGroupBox��$��15,0,0,0q����� $��LINE ASSOCIATION��
�������$��12(�.��)  #Microsoft.Windows.Controls.DataGrid  -   $��dgLine��$
��Left=�
��� � �$��0q�   AutoGenerateColumns .     HeadersVisibility$	 All=�$   HorizontalScrollBarVisibility$
 Auto=�"   VerticalScrollBarVisibility$
 Auto=�$��Center=�   CanUserAddRows .     CanUserSortColumns .     CanUserDeleteRows .     CanUserReorderColumns .  ���� $ LineAssociation�   Columns 7  1Microsoft.Windows.Controls.DataGridCheckBoxColumn  4  .Microsoft.Windows.Controls.DataGridBoundColumn   Binding �� $ IsAssociated�$	 TwoWay=�3  -Microsoft.Windows.Controls.DataGridTextColumn  /  )Microsoft.Windows.Controls.DataGridColumn   Header$
 LINE��   
Foreground
 �   �   FontSize$ 15(� �� $
 Name��� -	   $��shiftGroupBox��$��15,0,0,0q����� $��SHIFT ASSOCIATION��
�������$��12(�.��  -
   $��dgShift��$
��Left=�
��� � �$��0q� .  $ Column=�$��Center=� .   .   .   .  ���� $ ShiftAssociation�    �� $ IsAssociated�$	 TwoWay=�  $ SHIFT��
 �   �$ 15(� �� $
 Name��� -   $��departmentGroupBox��$��15,0,0,0q����� $��ISSUE ASSOCIATION��
�������$��12(�.��  -   $��dgDepartment��$
��Left=�
��� � �$��0q� .  $ Column=�$��Center=� .   .   .   .  ���� $ DepartmentAssociation�    �� $ IsAssociated�$	 TwoWay=�  $ ISSUE��
 �   �$ 15(� �� $
 Name��� -   $��escalationGroupBox��$��15,0,0,0q����� $��ESCALATION ASSOCIATION��
�������$��12(�.��  -   $��dgEscalation��$
��Left=�
��� � �$��0q� .  $ Column=�$��Center=� .   .   .   .  ���� $ EscalationAssociation�    �� $ IsAssociated�$	 TwoWay=�  $ 
ESCALATION��
 �   �$ 15(� �� $
 Name�