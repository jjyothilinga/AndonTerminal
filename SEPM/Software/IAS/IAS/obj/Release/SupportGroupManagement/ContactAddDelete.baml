   M S B A M L   `   `   `  ����   IAS    IAS.ContactAddDelete   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    >
wpfToolKit-http://schemas.microsoft.com/wpf/2008/toolkit  $
��Auto��$
��Auto��  � 	Resources  )  #Microsoft.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value��%�   '        '�� %     �� "  .���  � ��
 � � ��� " ��.���  � ��$ 20(���  � ��$ 10q���  � ��$ 50��.���� -   $��aMDGroupBox��
��� � �
�������$��2q����� 
�������.��]� .+��� $��Bottom=� �� Orientation$ 
Horizontal=�.+��� -   $��btnAdd��$��+��$��Bottom=�$��5q��� -   $��	btnDelete��$��-��$��Bottom=�$��5q�  -   $��dgItem��   AutoGenerateColumns .  
��� � �   
IsReadOnly .    SelectionMode$ Single=�   SelectionUnit$ FullRow=�	   HeadersVisibility$	 Column=�$��Center=�$
��Left=�$
   HorizontalScrollBarVisibility$

 Auto=�"   VerticalScrollBarVisibility$
 Auto=�$��0q�   CanUserAddRows .     CanUserDeleteRows .  ����    Columns 3  -Microsoft.Windows.Controls.DataGridTextColumn  /  )Microsoft.Windows.Controls.DataGridColumn   Header$
 NAME��   
Foreground
 �   �   FontSize$ 15(�4  .Microsoft.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$
 Name�