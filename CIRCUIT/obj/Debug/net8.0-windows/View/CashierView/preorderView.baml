   M S B A M L   `   `   `  ����B  >CIRCUIT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:CIRCUIT.View.CashierViewCIRCUIT.View.CashierView  5   	   +    %CIRCUIT.View.CashierView.preorderView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:CIRCUIT.View.CashierView     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ Preorder Transaction��5	   	   $	��500��5
   	   $	��700��6   ���#F4F4F4 � 
ResizeMode$ NoResize=�5   	    � 	Resources 5         HeaderTextStyle �� 
TargetType �� Property �� Value  
LabelStyle  InputControlStyle��%~  &	        &	 �     &	 �    �� 5   
   " ��6(   .�5      ��  � ��$ 20(�6)   �� 5       � ��$ SemiBold"�6+   �� 5       � �� �#2C3E506+   �� 5       � ��$ Center=�64   �� 5       � ��$
 0,20q�6'   5      �� 5   
   " ��6#   .�5      ��  � ��$ 14(�6)   �� 5       � �� �#34495E6+   �� 5       � ��$
 10,5q�6'   5      �� 5   
   " ��6*   .�5      ��  � ��$ 35��6'   �� 5        � ��$
 10,5q�6'   �� 5!       � ��$ 5q�6(   �� 5"       � ��$ 1q�60   �� 5#       � �� �#BDC3C76,   5$      5%      .��5'      � $��20q�6    � RowDefinitions 5(   
   �� 5)      $
��Auto �6   �� 5*      $
��Auto �6   �� 5+      $
��Auto �6   �� 5,      $
��Auto �6   �� 5-      $
��Auto �6   �� 5.      $
��Auto �6   �� 5/      $��* �6   .+�53   
   �� $��Preorder Transaction��6   #��[  54      $��0��55      �� 58   
   $��
Item Name:��6   #��[ 59      $��1��5:      �� 5;   
   -   5        $��ItemNameTextBox��5;      #��[ 5<      $��2��5=       �� 	MaxLength$	 100��5>      	 � ToolTip$1	 +Enter the name of the item to be preordered��5?      �� 5B   
   $��	Quantity:��6   #��[ 5C      $��3��5D      �� 5E   
   -   5        $��QuantityTextBox��5E      #��[ 5F      $��4��5G      $ 5��5I      $-	 'Enter the quantity of items to preorder��5J      �� 5O   
   $��Payment Method:��6   #��[ 5P      $��7��5Q      �� 5R   
   -   5        $��PaymentMethodComboBox��5R      #��[ 5S      $��8��5T      $	 Select the payment method��5U      .@�5V      �� $
��Cash��6   �� 5W      $
��Card��6   �� 5X      $��Online Payment��6   5Y      �� 5\   
   -   5        $��SubmitPreorderButton��5]      $��Submit Preorder��5\      $��6��5^      $	��150��5_      $��40��5`      $��
0,167,8,23q�5a      $��Right=�5b      ���#2ECC71
�������$��SemiBold"�5e      $��0q�5f      $%	 Submit the preorder transaction��5h      5i      5j      