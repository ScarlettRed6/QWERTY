   M S B A M L   `   `   `  ����B  >CIRCUIT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:CIRCUIT.ViewModelCIRCUIT.ViewModel  5   	   2    ,CIRCUIT.View.CashierView.ProductDetailsModal   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   .vmclr-namespace:CIRCUIT.ViewModel     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Product Details��5   	   $	��600��5   	   $	��800��5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�5   	   
������  � AllowsTransparency$
 True��5   	    � WindowStyle$
 None=�5   	    � DataContext 5      (   "CIRCUIT.ViewModel.NewSaleViewModel  5   
    � 	Resources 5         ProductDetailButton �� 
TargetType �� Property	 �� Value
 �� 
TargetType �� CornerRadius��%�  &	        �� 5   
   " ��6,   .�5      ��  � ��$		 150��6&   �� 5       � ��$	 40��6'   �� 5       � ��$	 14(�6)   �� 5       � ��$	 SemiBold"�6+   �� 5       � ��$	 10q�6'   �� 5       � ��	 5      �� 5      "
 ��6&   .R�5      �� #��z��6!   $ 20��5   !   $��1q�5    !   #��z��5!   !   ._�5"      �� $��Center=�6/   $��Center=�5#   -   5$      5%      5'      5(      5)      .��5+      �� $ 10��6     	palette10#��[ 5,      $��10q�5-      $��2q�5.      ��5/   
   �� 50       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 0,1&�63   .��51      �  � Color$ #FF8C00��6    � Offset$ 0Q�6/   � 52      $ #00C8FF��6   $
 0.50Q�6/   � 53      $ #FF8C00��6   $ 1Q�6/   54      ._�57   
   �  � RowDefinitions 58      �� 59      $
��Auto �6    �� 5:      $��* �6    �� 5;      $
��Auto �6    .+�5?      �� $��0��6   $��Product Details��5@      $��24(�5A      $
��Bold"�5B      
�������$��	0,10,0,20q�5D      $��Center=�5E      � 5H      $��1��6   $
��0,10q�6     � ColumnDefinitions 5I      �� 5J      $
��1.2* �6'   �� 5K      $��1* �6'   .+�5O      �� $��0��6   $��2q�5P        palette6#��[ 5Q      $ 10��5R      $��0,0,20,0q�5S      ._�5T      �� $��Uniform=�5U      $��5q�5V      ��5T      �� *	ImagePath+5V      5W      �� 5Z      $��1��6   $��Hidden=�6-   .+�5[      �� $��24(�5\      $
��Bold"�5]      
�������$
��Wrap=�5_      ��5[       �� *ProductName+5_      �� 5a      $��20(�5b      ���#4CAF50$
��0,10q�5d      ��5a       �� *SellingPrice+ �� StringFormat$ 	₱{0:N2}��5d      �� 5g        palette2#��[ 6   $ 10��5h       �� Padding$ 15q�5i      $
��0,10q�5j      ._�5k      �� $��15(�5l   "   $
��Wrap=�5m   "   
������� �� 
LineHeight$ 24��5o   "   ��5k   $   �� *Description+5o   "   5p      �� 5s      $��0,20,0,0q�6!   .+�5u      �� ���#FFD700$
��Bold"�5w   "   ��5u   $   �� *StockQuantity+$ 
Stock: {0}��5w   "   �� 5z      
�������$��Italic$�5|   "   ��5z   $   �� *
Category+$ Category: {0}��5|   "   �� 5      ���#FFB6C1$
��Bold"�5�   "   $	��0,5q�5�   "   ��5   $   �� *Brand+$ 
Brand: {0}��5�   "   5�      5�      5�      �� 5�      $��2��6    �� Orientation$ 
Horizontal=�5�      $��Center=�5�      $��	0,20,0,10q�5�      .+�5�      �� -   5        $��CANCEL��5�      $	��100��5�      $��50��5�        	RefundBtn#��[ 5�      5�      �� 5�      -   5        $��ADD TO CART��5�        StartNewSaleButton#��[ 5�      5�      5�      5�      5�      5�      