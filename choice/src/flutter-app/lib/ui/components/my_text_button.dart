import 'package:choice/ui/ui.dart';
import 'package:flutter/material.dart';

class MyTextButton extends StatelessWidget {
  const MyTextButton({
    super.key,
    required this.onTap,
    required this.text,
    this.padding = const EdgeInsets.only(top: 24),
  });

  final String text;
  final Function()? onTap;
  final EdgeInsets padding;

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Padding(
        padding: padding,
        child: InkWell(
          splashColor: Colors.white,
          borderRadius: BorderRadius.circular(10),
          onTap: onTap,
          child: Text(
            text,
            textAlign: TextAlign.center,
            style: AppTextStyles.textBtnTextStyle,
          ),
        ),
      ),
    );
  }
}
