import 'package:choice/ui/ui.dart';
import 'package:flutter/material.dart';

class MainButton extends StatelessWidget {
  const MainButton({
    super.key,
    required this.isEnabled,
    required this.text,
    this.onTap,
  });

  final bool isEnabled;
  final String text;
  final Function()? onTap;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 12, horizontal: 16),
      child: Opacity(
        opacity: isEnabled ? 1.0 : 0.4,
        child: GestureDetector(
          onTap: onTap,
          child: Container(
            height: 44,
            decoration: ShapeDecoration(
              color: const Color(0xFF2D81E0),
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(10),
              ),
            ),
            child: Center(
              child: Text(
                text,
                style: AppTextStyles.mainBtnTextStyle,
              ),
            ),
          ),
        ),
      ),
    );
  }
}
