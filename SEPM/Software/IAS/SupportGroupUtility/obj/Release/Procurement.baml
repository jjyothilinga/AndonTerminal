   M S B A M L   `   `   `  ����   SupportGroupUtility%    SupportGroupUtility.Procurement   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    6dg-http://schemas.microsoft.com/wpf/2008/toolkit  
��� � �.���   � ColumnDefinitions  �� $��* ��� $
��Auto �.+�)  #Microsoft.Windows.Controls.DataGrid  -   $��dgOpenIssuesGrid��$��0��   AutoGenerateColumns .     
IsReadOnly .    CanUserAddRows .     CanUserDeleteRows .  "   VerticalScrollBarVisibility$
 Auto=�$   HorizontalScrollBarVisibility$
 Auto=���. $
��Auto=�$
��Auto=�$y�Visible=�$
��Left=�$��0q�   CanUserSortColumns .  $��0,0,23.58,0q�����  � 	Resources @  :Microsoft.Windows.Controls.Primitives.DataGridColumnHeader	 �� 
TargetType
 �� Property �� Value��%�   '        �� H1��"	  .��� 
 � ��$ 25(��� 
 � ��$
 Bold"��� 
 � ��$ Center=��� 
 � ��$ 0q��� 
 � ��$
 Auto��   Columns 7  1Microsoft.Windows.Controls.DataGridTemplateColumn  /  )Microsoft.Windows.Controls.DataGridColumn   Header$ 	Record ID��   
IsReadOnly .    CanUserReorder .     
Visibility$ Hidden=�   CellTemplate �� .R��� $��25(�$
��bold"�$��0q�$��Center=�$��Center=����� *
RecordID+  $ Part No�� .  .   �� .R��� $��25(�$
��bold"�$��0q�$��Center=�$��Center=����� *PartNo+  $ Status�� .  .   �� .R��� $��25(�$
��bold"�$��0q�$��Center=�$��Center=����� *Status+�� -   $��btnAcknowledge��$��1��$��Acknowledge��$��5q�$��Bottom=�$y�Hidden=�