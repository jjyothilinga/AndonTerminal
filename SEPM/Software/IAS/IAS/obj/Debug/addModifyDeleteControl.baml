   M S B A M L   `   `   `  ����  IAS     IAS.addModifyDeleteControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      >
wpfToolKit-http://schemas.microsoft.com/wpf/2008/toolkit  5      $
��Auto��5      $
��Auto��6     � 	Resources  5      )  #Microsoft.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value��%b  '        '�� J     �� 5   
   "  6   .�5      ��  � ��
 � � �6+   5      �� 5   
   " ��6   .�5      ��  � ��$ 20(�6)   �� 5       � ��$ 10q�6'   �� 5       � ��$ 50��6)   �� 5       � ��$
 Auto��6&   �� 5       � ��$
 Auto��6'   �� 5       � ��$ Center=�64   5      5      .��5      �� -   5        $��aMDGroupBox��5      
��� � �
�������$��1q�5   #   $
��Auto��67   ��5   
   �� 5      
���������6   ��  �� RelativeSource �� *TemplatedParent+ �� Path$ Header�5       .��5$   
   �  � RowDefinitions 5%      �� 5&      $��* �6    �� 5'      $
��Auto �6!   .+�5)      �� $��1��6    �� Orientation$ 
Horizontal=�6&   $
��Left=�6@   $��Bottom=�6[   .+�5*      �� -   5        $��btnAdd��5*      $��+��6'   $��Bottom=�6H   $��5q�6c   �� 5,      -   5        $��	btnDelete��5,      $��-��6*   $��Bottom=�6N   $��5q�6i   5.        50      -   5        $��dgItem��50   /   $��0��6"      AutoGenerateColumns .  
��� � �	   
IsReadOnly	 . 
   SelectionMode$
 Single=�51   4      SelectionUnit$ FullRow=�6K      HeadersVisibility$ Column=�52   "   $��0q�6>   $
��Left=�53   "   $��Stretch=�6=   "   VerticalScrollBarVisibility$
 Auto=�55   !   $   HorizontalScrollBarVisibility$
 Auto=�6D   ��50   =   ��    Columns 56      3  -Microsoft.Windows.Controls.DataGridTextColumn  57      /  )Microsoft.Windows.Controls.DataGridColumn   Header$	  ID��58   5      
Foreground
 �   �   FontSize$ 15(�6U   4  .Microsoft.Windows.Controls.DataGridBoundColumn   Binding 57   5   �� $ ID�58   U   7  1Microsoft.Windows.Controls.DataGridTemplateColumn  5:         CellTemplate 5;      �� 5<      .R�5=   "   �� $��Stretch=�6,   $��15(�6h   $��50��5>   ,   ��5=   K   �� $
 Name�5>   ,   5?      5B      5H      5K      5N      5O      