   M S B A M L   `   `   `  ����O  Kmscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      E Aias.shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:ias.shared;assembly=ias.shared
ias.shared 5       
ias.client   ias.client.Window1   5   	   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      4sys&clr-namespace:System;assembly=mscorlib    5      <local,clr-namespace:ias.shared;assembly=ias.shared    5      -   5          � Title$  Intelligent Andon System ��5       � WindowState$ 	Maximized=�6'    � WindowStartupLocation$ CenterScreen=�6?    � 
ResizeMode$ CanMinimize=�5   
    � 	Resources 5      '  !ias.shared.statusToBackgroundConv   statusConverter��%   &	          5	   
   6A   5
      .��5      � 
��� � � � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5      � -   5        $��baseGrid��5      $��0��6   .+�5      �� -   5        $��tbMain��5      5      � 5   
   $��1��6   
��� � �.+�5      �� -   5        $��cMarquee��5       $��5q�6   }�. $��70��6D   $	��500��6P   
��� � �.+�5      �� -   5         ��Name$ 	tbMarquee��5      $��50(�6-   $��5q�6;   $��0q�5   #   
��� � �
�������.��5   I   5      5      5      5      5      