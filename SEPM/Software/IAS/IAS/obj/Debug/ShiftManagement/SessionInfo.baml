   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      > :IAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:IASIAS 5         IAS.SessionInfo   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      6sys&clr-namespace:System;assembly=mscorlib     5      #localclr-namespace:IAS     5        N� Title$  SessionInfo��5       � 	Resources 5	       �� 
TargetType �� Property �� Value��%�  '��       '�� m     '�� '    �� 5
   
   " ��6   .�5      ��  � ��$	 300��6)   �� 5       � ��$ 20(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$	 100��6)   �� 5       � ��$ Right=�64   �� 5       � ��
 �����6+   �� 5       � ��$ 20(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$ 30(�6)   5      5      .��5      �� 
��� � �.+�5   
   � -   5        $��nameGrid��5      $
��5,20q�6    $
��Auto��6.    � ColumnDefinitions 5      �� 5       $
��Auto �6#   �� 5!      $
��Auto �6#   .+�5#      �� $��0��6   $�� SESSION NAME:��6%   $
��Left=�6>   �� 5$      -   5        $��tbLineID��5$   &   $��1��6    �� 
IsReadOnly .  ��67   ��  �� Path$
 NAME� �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�5%      5'      � 5(   
   -   5        $��startTimeGrid��5(      $
��5,20q�6$   $
��Auto��62    5)      �� 5*      $
��Auto �6#   �� 5+      $
��Auto �6#   �� 5,      $
��Auto �6#   .+�5.      �� $��0��6   $�� START TIME:��6%   $
��Left=�6<   �� 5/      -   5        $��tbStartTime��5/   &   $��1��6    .  
 �� 	MaxLength$
 8��50   2   ��5/   :   �� $ 	StartTime�$ TwoWay=�$	 PropertyChanged=�50   2   �� 52      $��2��6   $�� [24Hr - HH:MM:SS]��6%   $
��Left=�6B   53      � 55   
   -   5        $��endTimeGrid��55      $
��5,20q�6"   $
��Auto��60    56      �� 57      $
��Auto �6#   �� 58      $
��Auto �6#   �� 59      $
��Auto �6#   .+�5;      �� $��0��6   $��
 END TIME:��6%   $
��Left=�6:   �� 5<      -   5        $��	tbEndTime��5<   &   $��1��6    .  $
 8��5=   2   ��5<   8   �� $ EndTime�$ TwoWay=�$	 PropertyChanged=�5=   2   �� 5?      $��2��6   $�� [24Hr - HH:MM:SS]��6%   $
��Left=�6B   5@      �� 5A   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   .+�5B      �� -   5        $��btnSave��5B      $
��Save��6$   $��5q�63   6>   �� 5C      -   5        $��	btnCancel��5C      $��Cancel��6&   $��5q�67   6B   5E      5G      5H      