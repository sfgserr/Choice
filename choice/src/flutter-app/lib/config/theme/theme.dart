
import 'package:choice/config/theme/colors.dart';
import 'package:choice/ui/utils/text_styles.dart';
import 'package:flutter/material.dart';

class AppTheme {

  static get lightTheme => ThemeData(
    useMaterial3: true,
    primaryColor: AppColors.primaryColor,
    progressIndicatorTheme: ProgressIndicatorThemeData(
      color: AppColors.linearLoaderBackgroundColor,
      linearTrackColor: AppColors.linearLoaderColor,
      linearMinHeight: 1.3,
    ),
    tabBarTheme: TabBarTheme(
      indicatorSize: TabBarIndicatorSize.label,
      indicatorColor: AppColors.primaryColor,
      labelPadding: EdgeInsets.zero,
      labelColor: Colors.black,
      labelStyle: AppTextStyles.tabTextStyle,
      unselectedLabelColor: AppColors.basicTextColor,
      unselectedLabelStyle: AppTextStyles.unselectedTabTextStyle,
    ),
    inputDecorationTheme: InputDecorationTheme(
      prefixStyle: AppTextStyles.hintTextStyle.copyWith(color: Colors.black),
      contentPadding: const EdgeInsets.all(12),
      hintStyle: AppTextStyles.hintTextStyle,
      filled: true,
      fillColor: AppColors.textFieldBackgroundColor,
      enabledBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(10),
        borderSide: const BorderSide(color: Colors.black12),
      ),
      focusedBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(10),
        borderSide: BorderSide(color: AppColors.primaryColor),
      ),
    ),
  );

  static get darkTheme => ThemeData(
    useMaterial3: true,
    brightness: Brightness.dark,
  );

}