   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  > :IAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:IASIAS    IAS.ShiftInfo   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    4sys&clr-namespace:System;assembly=mscorlib    !localclr-namespace:IAS      N� Title$  	ShiftInfo�� � 	Resources  �� 
TargetType �� Property �� Value��%�   '��       '�� :     '�� �     �� " ��.���  � ��$	 300����  � ��$ 20(��� " ��.���  � ��$	 100����  � ��$ Right=���  � ��
 �������  � ��$ 20(��� " ��.���  � ��$ 30(�.���� 
��� � �.+�� -   $��nameGrid��$
��5,20q�$
��Auto�� � ColumnDefinitions �� $
��Auto ��� $
��Auto �.+��� $��0��$�� SHIFT NAME:��$
��Left=��� -   $��tbLineID��$��1�� �� 
IsReadOnly .  ����  �� Path$ ID� �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�� -   $��startTimeGrid��$
��5,20q�$
��Auto�� �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$�� START TIME:��$
��Left=��� -   $��tbStartTime��$��1�� .  
 �� 	MaxLength$
 8������ $ 	StartTime�$ TwoWay=�$	 PropertyChanged=��� $��2��$�� [24Hr - HH:MM:SS]��$
��Left=�� -   $��endTimeGrid��$
��5,20q�$
��Auto�� �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��
 END TIME:��$
��Left=��� -   $��	tbEndTime��$��1�� .  $
 8������ $ EndTime�$ TwoWay=�$	 PropertyChanged=��� $��2��$�� [24Hr - HH:MM:SS]��$
��Left=���  �� Orientation$ 
Horizontal=�$��Center=�.+��� -   $��btnSave��$
��Save��$��5q��� -   $��	btnCancel��$��Cancel��$��5q�