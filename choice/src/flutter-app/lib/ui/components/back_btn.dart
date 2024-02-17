import 'package:auto_route/auto_route.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:choice/ui/utils/colors.dart';

class BackBtn extends StatelessWidget {
  const BackBtn({
    super.key,
    this.onTap,
  });

  final Function()? onTap;

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap ?? () => AutoRouter.of(context).pop(),
      child: Icon(
        CupertinoIcons.chevron_left,
        size: 28,
        color: AppColors.primaryColor,
      ),
    );
  }
}
