   M S B A M L   `   `   `  ���� E  Aias.client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ias.client
ias.client      ias.client.Summary   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    (localclr-namespace:ias.client    6dg-http://schemas.microsoft.com/wpf/2008/toolkit  
��� � �  � 	Resources   �� 
TargetType �� Property �� Value!   ias.client.Percentage2Width   availabilityConv��%  '��       '�� ;     '�� �     &	  �     �� " ��.���  � ��
 �������  � ��$ 15(��� " ��.���  � ��$ 15(���  � ��$ Center=���  � ��
 � � ���  � ��$ 0q��� " ��.���  � ��$ 15(���  � ��
 � ���  .���  � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $��* �.+�� $��0�� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+��� -   $��tbDate��$��0��$��15(�$��Right=��� $��1��$��25(�$��Center=�$��PLANT AVAILABILITY��$��10q�� $��2��$
��Auto��$��Center=� �� $
��Auto ��� $
��Auto �.+��� $��0��$�� Run Hours:��$
��Left=��� -   $��
tbRunHours��$��1������  �� Path$ RunHours�� $��3��$
��Auto��$��Center=� �� $
��Auto ��� $
��Auto �.+��� $��0��$��	Downtime:��$
��Left=��� -   $��
tbDowntime��$��1������ $ Downtime�� $��4��$
��Auto��$��Center=� �� $
��Auto ��� $
��Auto �.+��� $��0��$��Availability %:��$
��Left=��� -   $��tbAvailability��$��1������ $ Availability�]� -   $��
dkDowntime��$��5��$
��Auto��$
��Auto��$��10q�.+��� $	��Top=�$��DOWNTIME BREAKUP��$��Center=�$��25(�A�  A� Rows$ 1��$	��400��.+�� -   $��BreakdownGrid�� �� $��* ��� $
��Auto ��� $
��Auto �.+��� -   $��btnBreakdown��$��0��
���  ��$��Stretch=�$��Bottom=��� $��1��$��	Breakdown��$��Center=��� -   $��tbBreakdown��$��2��$��Center=�$��Bottom=�$
��Auto��$
��Auto��� -	   $��QualityGrid�� �� $��* ��� $
��Auto ��� $
��Auto �.+��� -
   $��
btnQuality��$��0��
��� ���$��Stretch=�$��Bottom=��� $��1��$��Quality��$��Center=��� -   $��	tbQuality��$��2��$��Center=�$��Bottom=�$
��Auto��$
��Auto��� -   $��PSGrid�� �� $��* ��� $
��Auto ��� $
��Auto �.+��� -   $��btnPS��$��0��
���**��$��Stretch=�$��Bottom=��� $��1��$��Part Shortage��$��Center=��� -   $
��tbPS��$��2��$��Center=�$��Bottom=�$
��Auto��$
��Auto���� $��25��$��1��$��Stretch=�$��Center=�
�������� $��2��.+�� 
��� � �.+�)  #Microsoft.Windows.Controls.DataGrid  -   $��LineStatsGrid��   AutoGenerateColumns .  	   CanUserReorderColumns	 .  
   CanUserSortColumns
 .     CanUserAddRows .     CanUserDeleteRows .     CanUserResizeColumns .  $	��Top=�$��Center=�   
IsReadOnly . ���� $ Lines�  @  :Microsoft.Windows.Controls.Primitives.DataGridColumnHeader��%�   '        �� H1��"  .���  � ��$ 12(���  � ��$
 Bold"���  � ��$ Center=���  � ��$ 0q���  � ��$
 Auto��   Columns 7  1Microsoft.Windows.Controls.DataGridTemplateColumn  /  )Microsoft.Windows.Controls.DataGridColumn   Header$
 Line��   
IsReadOnly .    CanUserReorder .     Width8  2Microsoft.Windows.Controls.DataGridLengthConverter$
 Auto    CellTemplate �� .R��� $��25(�$	��500"�
�������$��0q����� *Name+  $ Availability %�� .    
Visibility$ Visible=�$
 Auto  �� .R���  �� MaxLines$ 1�� �� 	MaxLength$ 6��$	��100��
�������$��15(�$��Center=�$��0q����� $ AvailablePercentage�  -   availabilityIndicatorCol��$  �� . $ Visible=�$ *  �� .R�� .+��  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��
��� � �$��Center=�$��25��$��0q���`�  `� 	Converter# [  .1��� $ AvailablePercentage��� $ ActualWidth� �� RelativeSource �� *TemplatedParent+�� $��1��
���  ��$��Center=�$��25��$��0q���`� # [  .1��� $ BreakdownPercentage��� $ ActualWidth� �� *TemplatedParent+�� $��2��
��� ���$��Center=�$��25��$��0q���`� # [  .1��� $ QualityPercentage��� $ ActualWidth� �� *TemplatedParent+�� $��3��
���**��$��Center=�$��25��$��0q���`� # [  .1��� $ PartshortagePercentage��� $ ActualWidth� �� *TemplatedParent+