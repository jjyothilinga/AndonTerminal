   M S B A M L   `   `   `  ����   IAS     IAS.addModifyDeleteControl   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    >
wpfToolKit-http://schemas.microsoft.com/wpf/2008/toolkit  $
��Auto��$
��Auto��  � 	Resources  )  #Microsoft.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value��%�   '        '�� %     �� "  .���  � ��
 � � ��� " ��.���  � ��$ 20(���  � ��$ 10q���  � ��$ 50����  � ��$
 Auto����  � ��$
 Auto����  � ��$ Center=�.���� -   $��aMDGroupBox��
��� � �
�������$��1q�$
��Auto������ 
�����������  �� RelativeSource �� *TemplatedParent+ �� Path$ Header�.���  � RowDefinitions �� $��* ��� $
��Auto �.+��� $��1�� �� Orientation$ 
Horizontal=�$
��Left=�$��Bottom=�.+��� -   $��btnAdd��$��+��$��Bottom=�$��5q��� -   $��	btnDelete��$��-��$��Bottom=�$��5q�  -   $��dgItem��$��0��   AutoGenerateColumns .  
��� � �	   
IsReadOnly	 . 
   SelectionMode$
 Single=�   SelectionUnit$ FullRow=�   HeadersVisibility$ Column=�$��0q�$
��Left=�$��Stretch=�"   VerticalScrollBarVisibility$
 Auto=�$   HorizontalScrollBarVisibility$
 Auto=�����    Columns 3  -Microsoft.Windows.Controls.DataGridTextColumn  /  )Microsoft.Windows.Controls.DataGridColumn   Header$	  ID��   
Foreground
 �   �   FontSize$ 15(�4  .Microsoft.Windows.Controls.DataGridBoundColumn   Binding �� $ ID�7  1Microsoft.Windows.Controls.DataGridTemplateColumn     CellTemplate �� .R��� $��Stretch=�$��15(�$��50������ $
 Name�