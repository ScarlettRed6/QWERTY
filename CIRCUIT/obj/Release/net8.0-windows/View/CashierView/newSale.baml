   M S B A M L   `   `   `  ����   CIRCUIT&     CIRCUIT.View.CashierView.NewSale   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  New Sale��$	��750��$
��1400�� � WindowStartupLocation$ CenterScreen=�.���  � ColumnDefinitions �� $��* ��� $	��350 �.+�� $��0��$��10q� � RowDefinitions �� $
��Auto ��� $��* �.+��� $��15(�$��0,0,0,0q�����  �� Path$ CurrentDateTime��� $��0��$��30��$��0,0,0,10q�$��1q����#CCCCCC$��5q�$��14(�$��SEARCH���� $��1��$
��Auto=�.��� .+��� -    ��Name$ ProductsItemsControl������ .R��� $	��150��$	��150��$��5q�$��1q����#CCCCCC �� CornerRadius$ 5��._��  �� $��2* ��� $
��Auto ��� $
��Auto �.+��� $��0��$��5q�$��Uniform=����� *ImageSource+�� $��1��$	��5,0q�$
��Wrap=� �� TextAlignment$ Center=����� *ProductName+�� $��2��$��5q�$ Center=�$
��Bold"����� *Price+���� .R�� � $��1�����#F5F5F5 �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$
��CART��$��20(�$
��Bold"�$��10q��� $��1��$��10q�.���� .+��� -   $ CartItemsControl������ .R�� $	��0,5q� �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0�� �� Orientation$ 
Horizontal=�.+��� $��-��$��25��$��25���� $��40��$	��5,0q�	 �� TextAlignment$	 Center=����� *
Quantity+�� $��+��$��25��$��25���� $��1��$��Center=�$
��10,0q����� *Price+�� $��2��$��Center=����� *
TotalPrice+� $��2��$��10q� �� $
��Auto ��� $
��Auto �.+��� $��0��$��0,1,0,1q����#CCCCCC
 �� Padding$

 0,10q�._�� .+��� $	��PHP��$
��Left=�$��16(��� $��2,720.00��$��Right=�$��16(�$
��Bold"�A� $��1�� A� Rows$ 2�� A� Columns$ 2��$��0,10,0,0q�.+��� $��CANCEL ORDER��$��2q�$��40�����#FFB6C1�� $��DISCOUNT��$��2q�$��40���� $��REFUND��$��2q�$��40���� $��PROCESS ORDER��$��2q�$��40�����#90EE90