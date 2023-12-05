import 'package:flutter/material.dart';

class InputWidgetModel {
  InputWidgetModel({
    required this.label,
    required this.hintText,
    this.showPrefix = false,
    this.showSuffix = false,
    this.autofocus = false,
    this.onChangeTextField,
    this.onFieldSubmitted,
    this.keyboardType = TextInputType.text,
    this.textInputAction = TextInputAction.done,
    required this.controller,
    this.focusNode,
    this.maxLength,
  });

  final String label;
  final String hintText;
  final Function(String)? onChangeTextField;
  final Function(String)? onFieldSubmitted;
  final bool showPrefix;
  final bool showSuffix;
  final bool autofocus;
  final TextInputType keyboardType;
  final TextInputAction textInputAction;
  final TextEditingController controller;
  final FocusNode? focusNode;
  final int? maxLength;
}
