   M S B A M L   `   `   `  ����B  >CIRCUIT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:CIRCUIT.ViewCIRCUIT.View  5   	   ?$clr-namespace:CIRCUIT.CustomControlsCIRCUIT.CustomControls  5   	   5clr-namespace:CIRCUIT.ViewModelCIRCUIT.ViewModel  5	   	        CIRCUIT.View.UserLoginView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:CIRCUIT.View     5   	   X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5   	   9customControls$clr-namespace:CIRCUIT.CustomControls  5   	   (vmclr-namespace:CIRCUIT.ViewModel  5	   	   -   5          � Icon$#  /Assets/Images/Circuit hd.png��5
   	    � Title$ UserLoginView��5   	   $	��550��6   $	��800��6,    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency$
 True��5   	    � DataContext 5      &    CIRCUIT.ViewModel.LoginViewModel  5   
    � 	Resources 5         BooleanToVisibility��%   &	        �� 5   
   6'   5      y�5      �� 5   
    �� Path$ IsViewVisible�6   	 �� Mode$	 TwoWay=�6'   
 �� 	Converter#
 [  65   .��5       ��  �� CornerRadius$ 12��6   ��5"   
   �� 5#       �� ImageSource$4 ./Assets/Images/Backgrounds/LoginBackground.jpg��6   ._�5%   
   �� $ 10��6   $��2,0,0,4q�5&      ��5'      �� 5(       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 0,1&�67   .��5)      �  � Color$ #552E00��6#    � Offset$ 0Q�63   � 5*      $ #AE445A��6#   $
 0.75Q�63   � 5+      $ #FF8C00��6#   $ 1Q�63   5,      ��5/      �� 50      $	 0,0&�6&   $	 1,0&�67    �� Opacity$	 0.8Q�6F   .��51      �   primary1# [ 6#   $
 0.10Q�6E   � 52      $ #002366��6#   $
 0.50Q�63   � 53      # [ 6#   $
 0.90Q�6E   54      ._�57      �  � RowDefinitions 58      �� 59      $��30 �6$   �� 5:      .+�5=      �  � ColumnDefinitions 5>      �� 5?      �� 5@      $��25 �6+   �� 5A      $��25 �6+   �� 5B      $��5 �6+   .+�5E      �� $��LOG IN��6      panelColor4#��[ 5F       $��20(�5G       $��Medium"�5H       $��0��5I       $��Center=�5J       $��10,0,0,0q�5K       �� 5M      -   5         ��Name$ btnMinimize��5M      $��1��5N      $��20��5O      $��25��5P        controlButton#��[ 5R        panelColor5#��[ 5S        panelColor2
 � Tag# [ 5T      .��5U      !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ WindowMinimize=�6'     controlButtonIcon#��[ 5V   '   5X      �� 5Z      -   5        $ btnClose��5Z      $��2��5[      $��20��5\      $��25��5]      #��[ 5_        panelColor1#��[ 5`      # [ 5a      .��5b        $ Close=�6'   #��[ 5c   '   5e      5g      �� 5j      $��1��6   $	��260��5k      $ 	15,0,15,0��5l      $��	0,30,0,20q�5m       �� Padding$ 10q�5n      ��5p      �� 5q      $	 0,0&�6.   $	 0,1&�6?   $	 0.9Q�6N   .��5r      � # [ 6+   $
 0.10Q�6M   � 5s      $ #002366��6+   $
 0.50Q�6;   � 5t      # [ 6+   $
 0.90Q�6M   5u      ._�5w      ��  �� Orientation$ Vertical=�6!   .+�5y      �� $#��/Assets/Images/Circuit hd.png��6    $	��100��5z       $	��100��6,   $��10q�69   �� 5|      $��CIRCUIT��6$     palette1#��[ 5}   $   $��25(�5~   $   $
��Bold"�5   $   $��Center=�5�   $   �� 5�      -   5         ��Name$ txtUser��5�   "    	 UsernameTextBoxStyle#��[	 5�   "   $��13(�5�   "   $��Medium"�5�   "    
 palette6#��[
 5�   "    �� 
CaretBrush
 ��ح�
�������$��0,0,0,1q�5�   "   $��28��5�   "   $��Center=�5�   "   $��0,40,0,0q�5�   "   ��5�   "   �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�5�   "   G� 5�      -   5         G�Name$ PasswordBox��5�   &     ModernPasswordBoxStyle#��[ 5�   &   $��28��5�   &   $��0,30,0,0q�5�   &   $��13(�5�   &   $��Medium"�5�   &   #��[
 5�   &    G� 
CaretBrush
 ��ح�
�������$��0,0,0,1q�5�   &   $��Center=�5�   &   5�   &   �� 5�      
�������$��12(�5�   $   $��Medium"�5�   $   $��0,10,0,0q�5�   $   $
��Wrap=�5�   $   ��5�   $   �� *ErrorMessage+$ PropertyChanged=�5�   $   �� 5�      -   5        $ btnLogin��5�   !   $��1��5�   !   $	��150��5�   !   $��LOGIN��5�   !   $
��Bold"�5�   !   $��40��5�   !     loginInButtonStyle#��[ 5�   !   # [ 5�   !   ��5�   !   �� *LoginCommand+$ PropertyChanged=�5�   !   5�      5�      5�      5�      5�      5�      