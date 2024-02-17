import 'package:choice/ui/ui.dart';
import 'package:flutter/material.dart';

class MainButton extends StatelessWidget {
  const MainButton({
    super.key,
    required this.isEnabled,
    required this.text,
    this.onTap,
    this.needPadding = false,
  });

  final bool isEnabled;
  final String text;
  final bool needPadding;
  final Function()? onTap;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.symmetric(vertical: 12, horizontal: needPadding ? 16 : 0),
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
