   M S B A M L   `   `   `  ����B  >CIRCUIT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:CIRCUIT.View.CashierViewCIRCUIT.View.CashierView  5   	   5clr-namespace:CIRCUIT.ViewModelCIRCUIT.ViewModel  5   	   0    *CIRCUIT.View.CashierView.discountModalView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:CIRCUIT.View.CashierView     5   	   .vmclr-namespace:CIRCUIT.ViewModel     5   	     � Title$  Authorization��5   	   $	��350��5	   	   $	��300��5
   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency$
 True��5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	    � DataContext 5      (   "CIRCUIT.ViewModel.NewSaleViewModel  5   
    � 	Resources 5         CustomButtonStyle �� 
TargetType �� Property  palette1	 �� Value
 �� 
TargetType �� Triggers P� Property P� Value  palette9  CustomComboBoxStyle  panelColor7  CustomPasswordBoxStyle��%j  &	        7  7  &	 �    7  &	 �    7  �� 5   
   " ��6*   .�5      ��  � ��8	   6+   �� 5       � ��
	 �����6+   �� 5       � ��$	 0q�60   �� 5       � ��$	 30��6'   �� 5       � ��$		 100��6&   �� 5       � ��	 5      �� 5       "
 ��6&   .R�5!      �� #��z��6!   #��z��5"   !   ._�5#      �� $��Center=�6/   $��Center=�5$   -   5%       5&      P� 5'       �@J�IsMouseOver$
 True��6=   . �5(   "   �� 8	   � ��6?   5)      5+      5-      5.      �� 50   
   " ��6,   .�51      ��  � ��8	   6+   �� 52       � ��
	 �����6+   �� 53       � ��$	 0q�60   54      �� 56   
   " G�6/   .�57      ��  � ��8	   6+   �� 58       � ��
	 �����6+   �� 59       � ��$	 0q�60   �� 5:       � ��$		 5,0q�6(   5;      5<      .��5>      ��   	palette10#��[ 6   $��0q�5?       �� CornerRadius$ 3��5@      ._�5A   
   � $��20q�6    � ColumnDefinitions 5B      �� 5C      $��7* �6#   �� 5D      $��9* �6#    � RowDefinitions 5F      �� 5G      $	��25* �6    �� 5H      $
��249* �6    .+�5J      �� $��2��6   $��2��6*   .+�5K      �� $��Authorization��6   $��Center=�5L      
�������$��16(�5N      $��0,0,0,20q�5O      �� 5Q      $��Avail for 20% Discount��6   $��Center=�5R      $
��Bold"�5S      $��20(�5T      
�������$��0,0,0,30q�5V      �� 5X      $��Authorization PIN��6   $��Center=�5Y      
�������$��0,0,0,5q�5[      G� 5]      -   5         G�Name$ PinPasswordBox��5]      $��30��5^        passWordBoxStyle#��[ 5_      $��0,0,0,20q�5`      �� 5b       �� Orientation$ 
Horizontal=�6   $��Right=�5c      .+�5d      �� -   5        $��Cancel��5d      #��[  5f      $��0,0,10,0q�5g      �� 5i      $��Apply Discount��6   #��[  5k      ��5j      �� *ApplyDiscountCommand+5k      5l      5m      5n      5o      5p      