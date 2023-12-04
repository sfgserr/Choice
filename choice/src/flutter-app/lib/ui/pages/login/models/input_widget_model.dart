import 'package:flutter/material.dart';

class InputWidgetModel {
  InputWidgetModel({
    required this.label,
    required this.hintText,
    this.showPrefix = false,
    this.showSuffix = false,
    this.obscureText = false,
    this.onSuffixIconTap,
    this.onChangeTextField,
    this.keyboardType = TextInputType.text,
    required this.controller,
  });

  final String label;
  final String hintText;
  final Function()? onSuffixIconTap;
  final Function(String)? onChangeTextField;
  final bool showPrefix;
  final bool showSuffix;
  final bool obscureText;
  final TextInputType keyboardType;
  final TextEditingController controller;
}
