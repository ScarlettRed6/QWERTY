   M S B A M L   `   `   `  ����B  >CIRCUIT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:CIRCUIT.ViewModel.CashierViewModel"CIRCUIT.ViewModel.CashierViewModel  5   	   1    +CIRCUIT.View.CashierView.CashierProfileView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?vm0clr-namespace:CIRCUIT.ViewModel.CashierViewModel     5   	   X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  6E     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ Cashier Profile��5   	   $	��450��6!   $	��700��6.    � WindowStyle$
 None=�5	   	   
������  � AllowsTransparency$
 True��5   	   .��5      ��  �� CornerRadius$ 10��6      	palette10#��[  5      $��2q�5      ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 0,1&�63   .��5      �  � Color$ #FF8C00��6    � Offset$ 0Q�6/   � 5      $ #FB2F2F��6   $
 0.50Q�6/   � 5      $ #FF8C00��6   $ 1Q�6/   5      ._�5   
   � $��20q�6   .+�5      �� -   5        $
��Left=�5      $	��Top=�5      $
��10,5q�5      $��40��5      $��30��5         	returnBtn#��[ 5!      .��5#      !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]	   Icon$	 ArrowAltCircleLeft=�6     
returnIcon#��[ 5$      $��35��5%      5&      � 5(      $��0,50,0,0q�6   
 � RowDefinitions
 5)      �� 5*      $
��Auto �6$   �� 5+      $��* �6$    � ColumnDefinitions 5-      �� 5.      $��2* �6'   �� 5/      $��3* �6'   .+�53      �� $��0��6   $��0��6&   $��2��66     palette1#��[ 54      $��2q�55      $��10q�56      $ 5��6%   ._�57      �� -   5        $��ProfileImage��57      $	��220��58      $	��300��6(   $��Uniform=�65   $��10q�6G   ��59      �� *ImagePathDisplay+5:      �� 5=      $��0��6   $��1��6*   $��10q�6:   $	��Top=�6F   .+�5>      �� $��
Full Name:��6    $��14(�62   
�������$
��Bold"�5@       $��0,0,0,5q�5A       �� 5B      $��5q�5C      $��35��5D        palette6#��[ 5E        generalTextBoxStyle#��[ 5F      ��5B      �� *
Fullname+ �� UpdateSourceTrigger$ PropertyChanged=�5F      5G      �� 5J      $��1��6   $��1��6*   $��10q�6:   $	��Top=�6F   .+�5K      �� $��Change Password:��6    
�������$��14(�6K   $
��Bold"�6Y   $��0,0,0,10q�6k   �� 5M      $��Current Password:��6    
�������$��SemiBold"�6M   $��0,0,0,3q�6c   G� 5N      -   5        $��CurrentPasswordBox��5N   "   $��0,0,0,10q�5P   "   $��5q�5Q   "   $��35��5R   "   #��[ 5S   "     generalPassBoxStyle#��[ 5T   "   �� 5V      $��New Password:��6    
�������$��SemiBold"�6H   $��0,0,0,3q�6^   G� 5W      -   5        $��NewPasswordBox��5W   "   $��0,0,0,10q�5Y   "   $��5q�5Z   "   $��35��5[   "   #��[ 5\   "   #��[ 5]   "   �� 5_      $��Confirm New Password:��6    
�������$��SemiBold"�6P   $��0,0,0,3q�6f   G� 5`      -   5        $��ConfirmNewPasswordBox��5`   "   $��0,0,0,10q�5b   "   $��5q�5c   "   $��35��5d   "   #��[ 5e   "   #��[ 5f   "   �� 5h      $��Save Changes��6   $	��150��5i      $
��Left=�5j      $
��10,5q�5k        StartNewSaleButton#��[ 5l      ��5m      �� *SaveChangesCommand+5n      5o      5p      5q      5r      