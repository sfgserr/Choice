import 'package:flutter/material.dart';

class ObscureTextIcon extends StatelessWidget {
  const ObscureTextIcon({super.key, required this.obscureText});

  final bool obscureText;

  @override
  Widget build(BuildContext context) {
    return obscureText
        ? const Icon(Icons.visibility_rounded)
        : const Icon(Icons.visibility_off_rounded);
  }
}
