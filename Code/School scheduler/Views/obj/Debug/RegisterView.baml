   M S B A M L   `   `   `  ����@  <Views, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ViewsViews  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5          Views.RegisterView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %viewsclr-namespace:Views     5      9system&clr-namespace:System;assembly=mscorlib     5        � 	Resources  5
      '   !Views.RadioButtonCheckedConverter   CheckedConverter  	BoolToVis  True 	 False��%�   &	        &	      &	 &     &	 B       5   
   6,   �� 5   
   6'   ��5   
   True��6,   ��5   
   	False��6.   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��2 �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��2 �6    � RowDefinitions 5   
   �� 5      $��2 �6   �� 5      $��24 �6   �� 5      $��2 �6   �� 5      $��24 �6   �� 5      $��2 �6   �� 5      $��24 �6   �� 5       $��2 �6   �� 5!      $��24 �6   �� 5"      $��2 �6   �� 5#      $��24 �6   �� 5$      $��2 �6   �� 5%      $��24 �6   �� 5&      $��2 �6   �� 5'      $
��auto �6   �� 5(      $��2 �6   �� 5)      $
��auto �6   �� 5*      $��2 �6   �� 5+      $��22 �6   �� 5,      $��2 �6   .+�5.   
   �� $��Mail:��6   $��1��6    $��1��60   �� 5/   
   $��2��6R   $��1��6b   $��3��6o    �� 
IsReadOnly . ��6   �� *	UserEmail+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 50   
   $��Name:��6   $��1��6    $��3��60   �� 51   
   $��2��6Q   $��3��6a   $��3��6n   ��6   �� *
UserName+$ PropertyChanged=�6n   �� 52   
   $��Address:��6   $��1��6#   $��5��63   �� 53   
   $��2��6T   $��5��6d   $��3��6q   ��6   �� *UserAddress+$ PropertyChanged=�6q   �� 54   
   $��	PostCode:��6   $��1��6$   $��7��64   �� 55   
   $��2��6U   $��7��6e   $��3��6r   ��6   �� *UserPostCode+$ PropertyChanged=�6r   �� 56   
   $��Phone:��6   $��1��6!   $��9��61   �� 57   
   $��2��6R   $��9��6b   $��3��6o   ��6   �� *	UserPhone+$ PropertyChanged=�6o   	� 59   
   $��2��6   $��11��6&   $��Center=�64   $��Student��6O    d� 	IsChecked 6a   ��  �� Path$ UserType� �� 	Converter# [   �� ConverterParameter$ Student��	 �� FallbackValue$
	 true��	� 5:   
   $��3��6   $��11��6&   $��Center=�64   $��Teacher��6O    6a   �� $ UserType�# [  $ Teacher��$	 false��� 5<   
   -   5        $��StudentInfo��5<      $��1��6"   $��13��62   $��4��6@   y�6T   �� $ UserType�# [  $ Student��$	 Visible�� 5=      �� 5>      $��2 �6#   �� 5?      $	��120 �6#   �� 5@      $��* �6#   �� 5A      $��2 �6#    5C      �� 5D      $��2 �6    �� 5E      $��24 �6    �� 5F      $��2 �6    �� 5G      $��24 �6    �� 5H      $��2 �6    .+�5J      �� $��Education StartDate:��6   $��1��63   $��1��6C   (  "System.Windows.Controls.DatePicker  5K      
   SelectedDateFormat$

 Long=�6   $��2��63   $��1��6C      SelectedDate 5L      �� *Education_StartDate+$ PropertyChanged=�
 �� Now �� TargetNullValue# Z   5N      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value��%[   '        �� 5O      "  6   .�5P      ��  �@��
IsReadOnly . 67   5Q      5R      5S      �� 5T      $��Education EndDate:��6   $��1��61   $��3��6A     5U      $

 Long=�6   $��2��63   $��3��6C    5V      �� $ Education_EndDate� �� Mode$ TwoWay=�# Z   5X      ��%[   '        �� 5Y      "  6   .�5Z      ��  �@��
IsReadOnly . 67   5[      5\      5]      5^      � 5_   
   -   5        $��TeacherInfo��5_      $��1��6"   $��13��62   $��4��6@   y�6T   �� $ UserType�# [  $ Teacher��$	 	Collapsed��.+�5`      �� ��6   �� *	Payrole+$ PropertyChanged=�5a      �� 5b   
   -   5        $��
Error_info��5b      $��1��6"   $��15��62   $��4��6@   $��26��6T   ��6`   �� *	ErrorText+$ PropertyChanged=�y�6�   �� *ErrorActive+$ PropertyChanged=�# [ $	 	Collapsed���� 5c   
   $��Register��65   $	��100��6H   $��3��6T   $��17��6d   $��2��6r   ��6   �� *RegisterCommand+6r   5d      5e      