   M S B A M L   `   `   `  ����  SupportGroupUtility!    SupportGroupUtility.Reports   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      6dg-http://schemas.microsoft.com/wpf/2008/toolkit  5      .��5   
   � 
��� � �  � RowDefinitions  5	      �� 5
      $
��Auto �6    �� 5      $
��Auto �6    �� 5      .+�5      �� $��0��6   $��Report Configuration��6$   $	��Top=�6B   ��5      �� 5      .R�5      �� 
�������$��Configuration��67   5      .��5      �   5      �� 5      $
��Auto �6(   �� 5      $
��Auto �6(   .+�5      A�  A� Rows$ 1��6"   $��0��6+   $��2q�68   $y�Hidden=�6D   .+�5      �� $��Report Type :��6    
�������68   �� 5      -   5        $��cmbViewTypeSelector��5   #   $
��Left=�6>    �� SelectedIndex$ -1��6Y   .@�5!      �� .��6+   Issue Report69   �� 5"      .��6+   Daily Summary6:   5#      5$      �� 5%      $��1��6!    �� Orientation$ 
Horizontal=�6.   $��5q�6G   .+�5&      �� -   5        $��lblFrom��5&       $��From:��6/   $
��Left=�6?   $��12(�6Z   $
��Auto��6i   
�������6v   +  %Microsoft.Windows.Controls.DatePicker  5'      -   5        $��dpFrom��5'   (   $��12(�66   $
��Auto��6D   $	��Top=�6R   $
��Left=�5(   (   $
��Auto��6C   �� 5)      -   5        $��lblTo��5)       $	��To:��6-   $
��Left=�6;   $��12(�6V   $
��Auto��6d   
�������6q     5*      -   5        $
��dpTo��5*   (   $��12(�64   $
��Auto��6B   $	��Top=�6P   $
��Left=�6h   �� 5+      -   5        $��btnGenerate��5+   !   $��5,0,0,0q�65   $��Generate��6F   6Y   �� 5,      -   5        $��	btnExport��5,   3   $��5,0,0,0q�6!   $��Export��6D   $��Right=�5-      $	��Top=�5.   !   $
��Auto��69   $
��Auto��5/   !   51      53      55      �� 56      $��1��6   $��Report��6$   $
��Auto��65   $
��Auto��6C   ��57      �� 58      .R�59      �� 
�������$��Report��67   5:      .��5<      �� .��5>      )  #Microsoft.Windows.Controls.DataGrid  -   5        $��dgReportGrid��5>   "      AutoGenerateColumns .    
IsReadOnly .    CanUserAddRows .     CanUserDeleteRows .  ��. $
��Auto=�5A      $
��Auto=�5B      $
��Left=�5C   >   $
��Auto��5D   #   $
��Auto��61   $y�Visible=�6>   ��5C   &   �� 5E      5F      5I      5N      5P      