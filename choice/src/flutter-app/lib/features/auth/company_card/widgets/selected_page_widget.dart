import 'package:choice/ui/ui.dart';
import 'package:flutter/material.dart';

class SelectedPageWidget extends StatelessWidget {
  const SelectedPageWidget({
    super.key,
    required this.selectedIndex,
  });

  final int selectedIndex;

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.symmetric(vertical: 16, horizontal: 11.5),
      child: Row(
        children: List.generate(
            3,
                (index) => Expanded(
              child: Container(
                margin: const EdgeInsets.symmetric(horizontal: 4.5),
                height: 4,
                decoration: BoxDecoration(
                  color: index <= selectedIndex
                      ? AppColors.blueColor
                      : AppColors.lightColor,
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
            )),
      ),
    );
  }
}