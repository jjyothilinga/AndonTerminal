   M S B A M L   `   `   `  ����   SupportGroupUtility!    SupportGroupUtility.Reports   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    6dg-http://schemas.microsoft.com/wpf/2008/toolkit  .��� 
��� � �  � RowDefinitions  �� $
��Auto ��� $
��Auto ��� .+��� $��0��$��Report Configuration��$	��Top=����� .R��� 
�������$��Configuration��.���   �� $
��Auto ��� $
��Auto �.+�A�  A� Rows$ 1��$��0��$��2q�$y�Hidden=�.+��� $��Report Type :��
��������� -   $��cmbViewTypeSelector��$
��Left=� �� SelectedIndex$ -1��.@��� .��Issue Report�� .��Daily Summary�� $��1�� �� Orientation$ 
Horizontal=�$��5q�.+��� -   $��lblFrom��$��From:��$
��Left=�$��12(�$
��Auto��
�������+  %Microsoft.Windows.Controls.DatePicker  -   $��dpFrom��$��12(�$
��Auto��$	��Top=�$
��Left=�$
��Auto���� -   $��lblTo��$	��To:��$
��Left=�$��12(�$
��Auto��
�������  -   $
��dpTo��$��12(�$
��Auto��$	��Top=�$
��Left=��� -   $��btnGenerate��$��5,0,0,0q�$��Generate���� -   $��	btnExport��$��5,0,0,0q�$��Export��$��Right=�$	��Top=�$
��Auto��$
��Auto���� $��1��$��Report��$
��Auto��$
��Auto������ .R��� 
�������$��Report��.���� .��)  #Microsoft.Windows.Controls.DataGrid  -   $��dgReportGrid��   AutoGenerateColumns .    
IsReadOnly .    CanUserAddRows .     CanUserDeleteRows .  ��. $
��Auto=�$
��Auto=�$
��Left=�$
��Auto��$
��Auto��$y�Visible=����� 