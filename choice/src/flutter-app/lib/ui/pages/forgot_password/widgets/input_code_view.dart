import 'dart:async';

import 'package:auto_route/auto_route.dart';
import 'package:choice/config/router/router.dart';
import 'package:choice/domain/models/ui_models/input_widget_model.dart';
import 'package:choice/ui/components/main_button.dart';
import 'package:choice/ui/components/input_widget.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:choice/ui/utils/text_styles.dart';
import 'package:flutter/material.dart';

import '../../../../domain/blocs/forgot_password_bloc/export_forgot_password_bloc.dart';

class FpCodeView extends StatefulWidget {
  const FpCodeView({super.key});

  @override
  State<FpCodeView> createState() => _FpCodeViewState();
}

class _FpCodeViewState extends State<FpCodeView> {
  late TextEditingController codeController;

  void setNewPassword() {
    BlocProvider.of<ForgotPasswordBloc>(context)
        .add(UpdateTimer(remainSeconds: 0));
    AutoRouter.of(context).push(const SetNewPasswordRoute());
  }

  @override
  void initState() {
    super.initState();
    codeController = TextEditingController();
  }

  @override
  void dispose() {
    codeController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<ForgotPasswordBloc, ForgotPasswordState>(
      builder: (context, state) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const SizedBox(
              height: 42,
            ),
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 16),
              child: Text(
                AppStrings.weSentCodeText + state.currentEmail!,
                style: AppTextStyles.bodySmallTextStyle,
              ),
            ),

            Container(
              alignment: Alignment.topLeft,
              padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
              child: InkWell(
                onTap: () {
                  BlocProvider.of<ForgotPasswordBloc>(context).add(
                    ChangeView(
                      isEmailView: true,
                      currentEmail: '',
                    ),
                  );
                },
                child: Text(
                  AppStrings.changeText,
                  style: AppTextStyles.textBtnTextStyle,
                ),
              ),
            ),

            // code
            InputWidget(
              inpwModel: InputWidgetModel(
                label: AppStrings.fpCodeText,
                hintText: AppStrings.inputFpCode,
                controller: codeController,
                keyboardType: TextInputType.number,
                onChangeTextField: (value) {
                  BlocProvider.of<ForgotPasswordBloc>(context).add(
                    EnableMainBtn(isEnabledMainBtn: value.isNotEmpty),
                  );
                },
                onFieldSubmitted: (value) {
                  BlocProvider.of<ForgotPasswordBloc>(context)
                      .add(UpdateTimer(remainSeconds: 61));
                },
              ),
            ),

            MainButton(
              isEnabled: state.isEnabledMainBtn,
              text: AppStrings.resetPassword,
              onTap: setNewPassword,
            ),

            const SizedBox(
              height: 26,
            ),

            // send code one more time
            state.remainSeconds > 0
                ? Padding(
                    padding: const EdgeInsets.only(top: 26),
                    child: Text(
                      AppStrings.resendCodeViaText(state.remainSeconds),
                      style: AppTextStyles.textBtnTextStyle.copyWith(
                        color: const Color(0xFF818C99),
                      ),
                    ),
                  )
                : Center(
                    child: Padding(
                      padding: const EdgeInsets.only(top: 26),
                      child: InkWell(
                        splashColor: Colors.white,
                        borderRadius: BorderRadius.circular(10),
                        onTap: () {
                          BlocProvider.of<ForgotPasswordBloc>(context)
                              .add(UpdateTimer(remainSeconds: 61));
                        },
                        child: Text(
                          AppStrings.sendCodeOneMoreTimeText,
                          textAlign: TextAlign.center,
                          style: AppTextStyles.textBtnTextStyle,
                        ),
                      ),
                    ),
                  )
          ],
        );
      },
    );
  }
}
