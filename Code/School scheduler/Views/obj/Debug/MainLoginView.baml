   M S B A M L   `   `   `  ����@  <Views, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ViewsViews  5          Views.MainLoginView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %viewsclr-namespace:Views     5        � 	Resources  5	         	BoolToVis��%   &	        �� 5
   
   6'   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��2 �6   �� 5      $��70 �6   �� 5      $��* �6   �� 5      $	��100 �6   �� 5      $��2 �6    � RowDefinitions 5   
   �� 5      $��2 �6   �� 5      $��24 �6   �� 5      $��2 �6   �� 5      $
��auto �6   �� 5      $��2 �6   �� 5      $��22 �6   �� 5      $��2 �6   .+�5   
   �� $��
User-mail:��6   $��1��6%   $��1��65   �� 5   
   $��2��6R   $��1��6b   $��2��6o   ��6   �� *	UserEmail+ �� UpdateSourceTrigger$ PropertyChanged=�6o   �� 5   
   -   5        $��
Error_info��5      $��1��6"   $��3��62   $��3��6?   $��26��6S   ��6_   �� *	ErrorText+$ PropertyChanged=�y�6�   �� *ErrorActive+$ PropertyChanged=� �� 	Converter# [   �� FallbackValue$ 	Collapsed���� 5    
   $��Login��62   $	��100��6B   $��1��6N   $��5��6^   $��2��6k   ��6   �� *LoginCommand+6k   �� 5!   
   $��Register��65   $	��100��6H   $��3��6T   $��5��6d   ��6   �� *RegisterCommand+6d   5"      5#      