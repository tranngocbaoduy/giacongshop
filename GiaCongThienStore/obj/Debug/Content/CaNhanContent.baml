   M S B A M L   `   `   `  ����L  HGiaCongThienStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:GiaCongThienStore.ContentGiaCongThienStore.Content  5      -    'GiaCongThienStore.Content.CaNhanContent   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:GiaCongThienStore.Content     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5
      U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5        $��CaNhanUC��5         MaterialDesignBody#���   5      $��Regular"�5      $��13(�5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5      .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedAccountWindowCommand+   CommandParameter 6S   ��  �� ElementName$ CaNhanUC��5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   .+�5!   
   � $��0��6   .+�5"      �� $��Thông tin cá nhân��6     HeadingContent#��[ 61   6[   5#      � 5%   
   $��1��6   $
��0 25q�6   	 � ColumnDefinitions	 5&      �� 5'      $��0.05* �6#   63   �� 5(      $��0.25* �6#   63   �� 5)      $��0.25* �6#   63   �� 5*      $��0.05* �6#   63   
 � 	Resources
 5,       �� 
TargetType �� Property �� Value �� RelativeSource �� Triggers ]� 
Conditions �� Property �� Value��%�  '�       '�� z     �� 5-      " �6   .�5.      ��  � ��$
 auto��6/   6?   �� 5/       � ��$
 auto��6.   6=   50      �� 51      " ��6   .�52      ��  � �� �#FFF6B   �� 53       � ��$	 200��6.   6<   �� 54       � �� 6/   �� *ActualWidth+ �� *Self+6u    55      ]� 56       57      �� 58   "    �@J�IsMouseOver . 6D   /�5:      �� 5;   "    � �� �#DCEEFF6Q   5=      5?      5@      .+�5A      � $��1��6   .+�5B      �� 
���   ���6   �� *NhanVien.TENNV+6Q   5C      5D      5E      5F      