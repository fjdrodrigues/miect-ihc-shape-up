   M S B A M L   `   `   `  ����B  >ShapeUp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ShapeUpShapeUp  5          ShapeUp.Food   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:ShapeUp     5        N� Title$
  Food��5	       N� ShowsNavigationUI .     
Page_Style#��[  5      #��� }�5      #��� ~�5      .��5      � $��10,10,10,10q�6    � RowDefinitions 5   
   �� 5      $
��40px �6   �� 5      $��1* �6   .+�5   
   � $��0��6   $��
0, 0, 0, 5q�6    � ColumnDefinitions 5      �� 5      $��1* �6#   �� 5      $
��20px �6#   .+�5      �� $��30, 0, 0, 0q�6   $��20(�6)   
�������$��	ExtraBold"�6^   $��0��6   .��6�   
Shape Up6�   �� 5      -   5        $��Minimize_Button��5   U     Minimize#��[ 63   $��1��6l   .��6|   -6   5      �� 5   
   $��0��6   ���#ffffff$��
0, 0, 0, 1q�64   6S   � 5   
   $��1��6    5      �� 5      $��3* �6#   �� 5       $	��11* �6#   .+�5"      � -   5        $��Logged_Menu��5"   #   $��0��6   $y�Visible=�66    5#      �� 5$      $	��5px �6$   �� 5%      $
��85px �6$   �� 5&      $
��85px �6$   �� 5'      $
��85px �6$   �� 5(      $
��85px �6$   �� 5)      $
��85px �6$   �� 5*      $��1* �6$   �� 5+      $
��65px �6$   �� 5,      $
��65px �6$   �� 5-      $
��65px �6$   �� 5.      $
��65px �6$   .+�51      �� -   5        $��Logged_Home_Button��51   g     Logged_Medium_Menu_Buttons#��[ 6   $��1��6�   .��6�   Home6�   �� 52      -   5        $��Account_Button��52   j   #��[ 6   $��2��6�   .��6�   	Account6�   �� 53      -   5        $��Exercises_Button��53   l   #��[ 6   $��3��6�   .��6�   	Exercises6�   �� 54      -   5        $��Food_Button��54   i    ' #Logged_Medium_Selected_Menu_Buttons#��[ 6   {�.  $��4��6|   .��6�   Food6�   �� 55      -   5        $��MyShapeUp_Button��55   l   #��[ 6   $��5��6�   .��6�   
My ShapeUp6�   �� 56      -   5        $��Logged_About_Button��56   g     Logged_Small_Menu_Buttons#��[ 6   $��8��6�   .��6�   About6�   �� 57      -	   5        $��Logout_Button��57   h   #��[ 6   $��9��6}   .��6�   Logout6�   �� 58      -
   5        $��Logged_Exit_Button��58   e     Logged_Small_Exit_Button#��[ 6   $��10��6   .��6�   Exit6�   59      � 5:      $��1��6    5;      �� 5<      $��200px �6$   �� 5=      $��1* �6$   �� 5>      $
��50px �6$   �� 5?      $��100px �6$   .+�5A      ��   Login_Label#��[ 6   $��0��6=   .��6K   Food6Q   &   System.Windows.Controls.DataGrid  5C      -   5        $��Food_DataGrid��5C   (   $��1��6     Datagrid_Style#��[ 5D        Datagrid_ColumnHeader_Style   ColumnHeaderStyle# [ 5E       	 Datagrid_Row_Style   RowStyle# [	 5F       
 Datagrid_Cell_Style   	CellStyle# [
 5G         AutoGenerateColumns . 5I   d   �� 5J      -   5        $��Empty_DataGrid_Label��5J   %   $��1��6     Empty_Datagrid_Style#��[ 5K      .��6G   " No Receipts currently available.6i   � 5L      $��2��6    5M      �� 5N      $	��150 �6+   �� 5O      $	��240 �6+   �� 5P      $��1* �6+   �� 5Q      $	��200 �6+   �� 5R      $	��150 �6+   .+�5T      �� -   5        $!��AddReceipt_MyShapeUp_Button��5T   �     Add_MyShapeUp_Button#��[ 6H   $��1��6v   .��6�   Add to My ShapeUp6�   �� 5U      -   5        $'��!Adding_Receipt_To_MyShapeUp_Error��5U       ( $Adding_Item_To_MyShapeUp_Error_Style#��[ 5V      $��2��5W      .��6-   Item already in My ShapeUp!6J   �� 5X      -   5        $.��(Adding_Receipt_To_MyShapeUp_Confirmation��5X       / +Adding_Item_To_MyShapeUp_Confirmation_Style#��[ 5Y      $��2��5Z      .��6-   Receipt added to My ShapeUp!6K   �� 5[      -   5        $��Suggest_Receipt_Button��5[   �     Make_Suggestion_Button#��[ 6C   $��3��6s   .��6�   Make Suggestion6�   5\      5]      5^      5_      5`      