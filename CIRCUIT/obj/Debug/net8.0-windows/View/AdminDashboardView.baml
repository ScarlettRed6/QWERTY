   M S B A M L   `   `   `  ����B  >CIRCUIT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:CIRCUIT.View.AdminDashboardViewCIRCUIT.View.AdminDashboardView  5   	   5clr-namespace:CIRCUIT.ViewModelCIRCUIT.ViewModel  5   	   I)clr-namespace:CIRCUIT.View.AdminDashboardCIRCUIT.View.AdminDashboard  5   	   Q-clr-namespace:CIRCUIT.View.AdminDashboardViewCIRCUIT.View.AdminDashboardView  5	   	   S.clr-namespace:CIRCUIT.View.AdminDashboardViews CIRCUIT.View.AdminDashboardViews  5
   	   e7clr-namespace:CIRCUIT.ViewModel.AdminDashboardViewModel)CIRCUIT.ViewModel.AdminDashboardViewModel  5   	   8    2CIRCUIT.View.AdminDashboardView.AdminDashboardView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?local-clr-namespace:CIRCUIT.View.AdminDashboardView     5   	   .vmclr-namespace:CIRCUIT.ViewModel     5   	   ;Views)clr-namespace:CIRCUIT.View.AdminDashboard     5   	   @Views2-clr-namespace:CIRCUIT.View.AdminDashboardView     5	   	   AViews3.clr-namespace:CIRCUIT.View.AdminDashboardViews     5
   	   KadminVm7clr-namespace:CIRCUIT.ViewModel.AdminDashboardViewModel     5   	   X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5   	     � Icon$#  /Assets/Images/Circuit hd.png��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�6.   
������  � AllowsTransparency$
 True��5   "    � Title$ AdminDashboardView��5   	   $	��800��6$   $
��1400��61    � 	Resources 5       �� DataType=   7CIRCUIT.ViewModel.AdminDashboardViewModel.HomeViewModel*   $CIRCUIT.View.AdminDashboard.HomeViewE   ?CIRCUIT.ViewModel.AdminDashboardViewModel.AnalyticsAndReportsVM3   -CIRCUIT.View.AdminDashboardView.AnalyticsView@   :CIRCUIT.ViewModel.AdminDashboardViewModel.CatalogViewModel-   'CIRCUIT.View.AdminDashboard.CatalogViewI   CCIRCUIT.ViewModel.AdminDashboardViewModel.ArchivedProductsViewModel;   5CIRCUIT.View.AdminDashboardViews.ArchivedProductsView>	   8CIRCUIT.ViewModel.AdminDashboardViewModel.StaffViewModel0
   *CIRCUIT.View.AdminDashboardViews.StaffViewJ   DCIRCUIT.ViewModel.AdminDashboardViewModel.SalesTransactionsViewModel<   6CIRCUIT.View.AdminDashboardViews.SalesTransactionsViewE   ?CIRCUIT.ViewModel.AdminDashboardViewModel.StockControlViewModel7   1CIRCUIT.View.AdminDashboardViews.StockControlView��%  (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)(�� �     5!      *, +)(�� �     5%      *,	 +)(�� �     5)      *, +)(�� ,    5-      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5!   
   "  6   .R�5"        5#      �� 5%   
   " 	 6   .R�5&      
  5'      �� 5)   
   "  6   .R�5*        5+      �� 5-   
   "  6   .R�5.        5/      51      .��55      ��  �� CornerRadius$ 10��6   $��3,2,2,5q�6   ��57   
   �� 58       �� 
StartPoint$	 0,0&�6"   	 �� EndPoint$		 0,1&�63   .��59      � 
 � Color$
 #000B22��6    � Offset$ 0Q�6/   � 5:      $
 #AE445A��6   $
 0.75Q�6/   � 5;      $
 #FF8C00��6   $ 1Q�6/   5<      ��5>   
   �� 5?       �� Color$ #001744��6   ._�5B   
   �  � ColumnDefinitions 5C      �� 5D      $	��250 �6#   �� 5E      $��* �6#   .+�5I      �� $ 7,0,0,5��6   ��5J      �� 5K      $ #000B22��6&   ._�5N      �  � RowDefinitions 5O      �� 5P      �� 5Q      $��80 �6(   .+�5T      �� .+�5W      ��  �� Orientation$ 
Horizontal=�6%   $��35��6>   $��
15,25,0,40q�5X   %   .+�5Z      �� $#��/Assets/Images/Circuit hd.png��6$   $��30��6K   �� 5[      $��CIRCUIT��6(      palette1#��[  67   $��20(�5\   (   $��Medium"�66   $��Center=�5]   (   $��10,0,0,0q�6C   5_      �� 5a      $��	Dashboard��6$     panelColor4#��[ 5b   $   $��15(�5c   $   $��Medium"�5d   $   $��	30,0,0,10q�5e   $   	� 5h        
menuButton#��[ 6&   
 � Tag# [  5i   &    d� 	IsChecked$
 TrueY�5k   &   ��5j   &   �� *ShowHomeCommand+.��5m      �� $ 
Horizontal=�6)   .+�5n   "   !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ ChartBar=�6/     menuButtonIcon#��[ 6?   �� 5o   "   $��Reports��6,     menuButtonText#��[ 6;   5p      5r      	� 5�      #��[ 6&   # [  5�   &   ��5�   &   �� *ShowSalesCommand+.��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ ExchangeAlt=�6/   #��[ 6B   �� 5�   "   $��Sales��6,   #��[ 69   5�      5�      	� 5�      #��[ 6&   # [  5�   &   ��5�   &   �� *ShowCatalogCommand+.��5�      �� $ 
Horizontal=�6)   .+�5�   "     $	 Tag=�6/   #��[ 6:   �� 5�   "   $��Products��6,   #��[ 6<   5�      5�      	� 5�      #��[ 6&   # [  5�   &   ��5�   &   �� *ShowStockControlCommand+.��5�      �� $ 
Horizontal=�6)   .+�5�   "     $	 Box=�6/   #��[ 6:   �� 5�   "   $��Stock��6,   #��[ 69   5�      5�      	� 5�      #��[ 6&   # [  5�   &   ��5�   &   �� *ShowArchivedCommand+.��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ Archive=�6/   #��[ 6>   �� 5�   "   $��Archived��6,   #��[ 6<   5�      5�      �� 5�      $��2��6$   
��� ���$��	10,0,10,0q�5�   $   	� 5�      #��[ 6&   # [  5�   &   ��5�   &   �� *ShowEmployeeManagementCommand+.��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ PeopleGroup=�6/   #��[ 6B   �� 5�   "   $��Staff��6,   #��[ 69   5�      5�      �� 5�      -   5         ��Name$ 	logoutBtn��5�   !   $��25��5�   !   $	��140��5�   !   $��15(�5�   !   $��Medium"�5�   !     logoutButtonStyle#��[ 5�   !   ��5�   !   �� *LogOutBtnCommand+.��5�      �� $ 
Horizontal=�6)   $��0,0,40,0q�5�   )   .+�5�   "     $ SignOut=�6/     
logoutIcon#��[ 6>   �� 5�   "   $��Log out��6,     
logoutText#��[ 6;   5�      5�      5�      5�      5�      � 5�      $��1��6    5�      �� 5�      $��25 �6$   �� 5�      $��35 �6$   �� 5�      $��* �6$   .+�5�      �� -   5         ��Name$ 
ControlBar��5�      $ 
Horizontal=�61   $��RightToLeft=�5�      
������ $��0,3,5,0q�5�      .+�5�      �� -   5        $ btnClose��5�        controlButton#��[ 6/    	 panelColor2# [	 5�      ��6@   �� *btnCloseCommand+.��5�        $ Xmark=�6'    
 controlButtonIcon#��[
 5�   '   5�      �� 5�      -   5        $ btnMaximize��5�      #��[ 62   # [	 5�      ��6@   �� *btnMaximizeCommand+.��5�        $ Square=�6'   #��[
 5�   '   5�      �� 5�      -   5        $ btnMinimize��5�      #��[ 62   # [	 5�      ��6@   �� *btnMinimizeCommand+.��5�        $ WindowMinimize=�6'   #��[
 5�   '   5�      5�      � 5�      $��1��6    5�      �� 5�      �� 5�      .+�5      �� $ 
Horizontal=�6!   $��Center=�5  !   .+�5       $��20��6=   $��20��5  '      
Foreground# [  62   $��	35,0,10,0q�5  '    5  '   �� *Icon+5  '   �� 5       palette6#��[ 6=   $��20(�5  $   $��Medium"�62   $��Center=�5  $   ��5  $   �� *	Caption+5  $   5	     �� 5     $��1��6!   $ 
Horizontal=�61   $��LeftToRight=�5  !   $��Center=�5  !   $��Center=�5  !   $��10,0,0,0q�5  !   .+�5     �� $��	WELCOME, ��6$   #��[ 65   $��20(�5  $   $��Center=�62   $��Medium"�5  $   �� 5     #��[ 5  $   $��20(�5  $   $��Center=�62   $��Medium"�5  $   ��5  $   �� *LoggedInAdmin+5  $   5     5     �� 5     $��2��6!   $��10q�5   !   ��5  .   �� *CurrentAdminView+5   !   5"     5$     5&     5(     