   M S B A M L   `   `   `  ����   
ias.client    ias.client.Monitor   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    6dg-http://schemas.microsoft.com/wpf/2008/toolkit  .��� 
��� � �.+�(� $	��Top=�.��)  #Microsoft.Windows.Controls.DataGrid  -   $��LineStatsGrid��    AutoGenerateColumns  .     CanUserReorderColumns .     CanUserSortColumns .     CanUserAddRows .     CanUserDeleteRows .     CanUserResizeColumns .  $	��Top=�$��Center=�   
IsReadOnly . ����  � 	Resources @  :Microsoft.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType	 �� Property
 �� Value��%�   '        �� H1��"  .��� 	 � ��$
 12(��� 	 � ��$

 Bold"��� 	 � ��$
 Center=��� 	 � ��$
 0q��� 	 � ��$

 Auto��   Columns 7  1Microsoft.Windows.Controls.DataGridTemplateColumn  /  )Microsoft.Windows.Controls.DataGridColumn   Header$ Production Line��   
IsReadOnly .    CanUserReorder .     CellTemplate �� .R��� $��25(�$	��500"�$��0q����� *LineDescription+  $ 
Target Qty��   
Visibility$ Hidden=� .  �� .R���  �� MaxLines$ 1��$��35(�$��Center=�$��80��$��0q�$��Right=����� *TargetQuantity+  $ Produced Qty��$ Hidden=� .  �� .R��� $ 1��$��35(�$��80��$��Center=�$��0q�$��Right=����� *ActualQuantity+  $ 	BreakDown�� . $ Visible=� �� .R��� $ 1�� �� 	MaxLength$ 6��$	��100��$��15(�$��Center=�$��0q�����  �� Path$ BreakdownBrush�  $ Quality�� . $ Visible=� �� .R��� $ 1��$ 6��$	��100��$��15(�$��0q����� $ QualityBrush�  $ Material Shortage�� .  �� .R��� $ 1��$ 4��$	��100��$��15(�$��0q����� $ MaterialShortageBrush�