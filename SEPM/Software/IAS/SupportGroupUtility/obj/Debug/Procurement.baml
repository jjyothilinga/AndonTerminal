   M S B A M L   `   `   `  ����  SupportGroupUtility%    SupportGroupUtility.Procurement   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      6dg-http://schemas.microsoft.com/wpf/2008/toolkit  5      
��� � �.��5      �   � ColumnDefinitions  5      �� 5      $��* �6'   �� 5	      $
��Auto �6'   .+�5      )  #Microsoft.Windows.Controls.DataGrid  -   5        $��dgOpenIssuesGrid��5   .   $��0��6      AutoGenerateColumns .     
IsReadOnly .    CanUserAddRows .     CanUserDeleteRows .  "   VerticalScrollBarVisibility$
 Auto=�5   B   $   HorizontalScrollBarVisibility$
 Auto=�6e   ��. $
��Auto=�5   N   $
��Auto=�5      $y�Visible=�6A   $
��Left=�6V   $��0q�5         CanUserSortColumns .  $��0,0,23.58,0q�6v   ��5      ��  � 	Resources 5      @  :Microsoft.Windows.Controls.Primitives.DataGridColumnHeader	 �� 
TargetType
 �� Property �� Value��%�   '        �� 5      H1��"	  6,   .�5      �� 
 � ��$ 25(�69   �� 5      
 � ��$
 Bold"�6;   �� 5      
 � ��$ Center=�6D   �� 5      
 � ��$ 0q�6@   �� 5      
 � ��$
 Auto��66   5      5         Columns 5      7  1Microsoft.Windows.Controls.DataGridTemplateColumn  5      /  )Microsoft.Windows.Controls.DataGridColumn   Header$ 	Record ID��64      
IsReadOnly .    CanUserReorder .     
Visibility$ Hidden=�6r      CellTemplate 5       �� 5!   "   .R�5"   &   �� $��25(�6H   $
��bold"�6V   $��0q�5#   =   $��Center=�6Q   $��Center=�5$   .   ��5"   .   �� *
RecordID+5$   .   5%   #   5'        5(      $ Part No��64    .  .   5)      �� 5*   "   .R�5+   &   �� $��25(�6F   $
��bold"�6T   $��0q�5,   =   $��Center=�6Q   $��Center=�5-   .   ��5+   .   �� *PartNo+5-   .   5.   #   50        51      $ Status��64    .  .   52      �� 53   "   .R�54   &   �� $��25(�6F   $
��bold"�6T   $��0q�55   =   $��Center=�6Q   $��Center=�56   .   ��54   .   �� *Status+56   .   57   #   59      5;      �� 5>      -   5        $��btnAcknowledge��5>   )   $��1��6   $��Acknowledge��6?   $��5q�6U   $��Bottom=�5?      $y�Hidden=�65   5D      5E      