   M S B A M L   `   `   `  ����B  >CIRCUIT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:CIRCUIT.ViewModelCIRCUIT.ViewModel  5   	   *    $CIRCUIT.View.CashierView.CashierView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   .vmclr-namespace:CIRCUIT.ViewModel     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  6P   -   5          � WindowStyle$
  None=�5   	    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	   
������  � AllowsTransparency$
 True��5   	    � Title$ CashierView��5   	   $	��600��6   $	��800��6*    � DataContext 5      (   "CIRCUIT.ViewModel.NewSaleViewModel  5   
   .��5      ��  �� CornerRadius$ 10��6   $��3,2,2,5q�6   ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 0,1&�63   .��5       � 	 � Color$	 #000B22��6   
 � Offset$
 0Q�6/   � 5!      $	 #AE445A��6   $

 0.75Q�6/   � 5"      $	 #FF8C00��6   $
 1Q�6/   5#      ��5%   
   �� 5&       �� Color$ #001744��6   ._�5)   
   �  � RowDefinitions 5*      �� 5+      $��25 �6    �� 5,      $
��Auto �6    �� 5-      $��* �6    �� 5.      $
��Auto �6    .+�51      �� -   5         ��Name$ 
ControlBar��51       �� Orientation$ 
Horizontal=�6-   $��RightToLeft=�52      
������ $��0,3,5,0q�53      .+�58      �� -   5         ��Name$ btnClose��58         controlButton#��[  6+     panelColor2
 � Tag# [ 59      ��6<   �� *BtnCloseCommand+.��5:      !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ Xmark=�6#     controlButtonIcon#��[ 5;   #   5=      �� 5?      -   5        $ btnMaximize��5?      #��[  6.   # [ 5@      .��5A        $ Square=�6#   #��[ 5B   #   5D      �� 5F      -   5        $ btnMinimize��5F      #��[  6.   # [ 5G      .��5H        $ WindowMinimize=�6#   #��[ 5I   #   5K      5M      �� 5Q      $��80��6   $	��Top=�6!   $��0,90,0,0q�69   $��1��6K   ._�5R      � $��Center=�6   .+�5S      �� $��WELCOME ��6    $��36(�60   $��
Montserrat)�6>   $
��Bold"�6V     palette6#��[ 6h   $��Center=�6�   �� 5T      $��36(�6;   $
��Bold"�6I   #��[ 6[   $��Center=�6�   $��Center=�6�   $��0,30,0,0q�6�   ��6    �� *	StaffName+6�   5U      5V      �� 5Y      $��50��6   $��1��6!   $	��Top=�6.   $��0,10,0,0q�6F   $ 2��6X    J� Effect 5Z      3  -System.Windows.Media.Effects.DropShadowEffect  5[         ShadowDepth$ 1Q�6'      
BlurRadius$ 4Q�67      Opacity$	 0.2Q�6F   ._�5]      �  � ColumnDefinitions 5^      �� 5_      $��2* �6+   �� 5`      $��0* �6+   �� 5a      $��2* �6+   .+�5d      � $��0��6   $��Center=�6+    5e      �� 5f      $
��Auto �6/   �� 5g      $��* �6/   5i      � 5k      $��2��6   $��Center=�6+   $��Right=�6F    5l      �� 5m      $
��Auto �6/   �� 5n      $
��Auto �6/   .+�5q      �� $��Date: ��6$   $��Center=�62   $
��10,0q�6M   $
��Bold"�6[   $��0��6m   #��[ 6}   �� 5r      $��12(�5s   "   $��5,0,10,0q�60   $
��Bold"�6B   $��1��6T   #��[ 6d   ��5r   $   �� *CurrentDate+ �� StringFormat$ {0:MM/dd/yyyy}��5s   d   5t      5u      5v      �� 5y      $��2��6   $��Center=�6&   $��Center=�6A   .+�5z      �� $ Vertical=�6   $��Center=�64   $��Center=�6Q   .+�5|      �� -   5        $ newSale��5|      $��25(�5}      $��90��5~      $	��370��5      $��Start New Sale��5�        StartNewSaleButton#��[ 5�      5�      �� 5�      -   5        $ History��5�      $��25(�5�      $��90��5�      $	��370��5�      $��View Transaction History��5�      $��10q�5�        ViewTransactionHistoryBtn#��[ 5�      5�      5�      5�      �� 5�      -   5        $ 	logoutBtn��5�      $��3��5�      $��50��5�      $	��130��5�      $��20(�5�      $��Medium"�5�      $��Right=�5�      $��10q�5�      $��Log out��5�        ExportButtonStyle#��[ 5�      #��[ 5�      ��5�      �� *LogOutBtnCommand+5�      5�      5�      5�      