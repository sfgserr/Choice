import 'package:flutter/material.dart';
import 'package:choice/ui/components/app_logo_widget.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:choice/ui/utils/text_styles.dart';

class AppMainInfoWidget extends StatelessWidget {
  const AppMainInfoWidget({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        const AppLogoWidget(),
        const SizedBox(
          height: 34,
        ),
        Text(
          AppStrings.appName,
          textAlign: TextAlign.center,
          style: AppTextStyles.appNameTextStyle,
        ),
        const SizedBox(
          height: 10,
        ),
        SizedBox(
          width: 228,
          child: Text(
            AppStrings.appLogoText,
            textAlign: TextAlign.center,
            style: AppTextStyles.logoTextStyle,
          ),
        ),
      ],
    );
  }
}
