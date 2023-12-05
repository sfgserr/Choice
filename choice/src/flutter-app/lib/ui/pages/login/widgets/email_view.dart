import 'package:auto_route/auto_route.dart';
import 'package:choice/config/router/router.dart';
import 'package:choice/domain/blocs/login_bloc/export_login_bloc.dart';
import 'package:choice/ui/components/main_button.dart';
import 'package:choice/ui/pages/login/models/input_widget_model.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:choice/ui/utils/text_styles.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../../splash/splash_screen.dart';
import 'input_widget.dart';

class EmailView extends StatefulWidget {
  const EmailView({super.key});

  @override
  State<EmailView> createState() => _EmailViewState();
}

class _EmailViewState extends State<EmailView> {
  late TextEditingController emailController;
  late TextEditingController passwordController;
  late FocusNode emailFocus;
  late FocusNode passwordFocus;

  @override
  void initState() {
    super.initState();
    emailController = TextEditingController();
    passwordController = TextEditingController();
    emailFocus = FocusNode();
    passwordFocus = FocusNode();
  }

  @override
  void dispose() {
    emailController.dispose();
    passwordController.dispose();
    emailFocus.dispose();
    passwordFocus.dispose();
    super.dispose();
  }

  void loginTap() {
    FocusScope.of(context).unfocus();
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) => const SplashScreen(),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    bool isKeyboardVisible = Provider.of<bool>(context);
    return BlocBuilder<LoginBloc, LoginState>(
      builder: (context, state) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // email
            InputWidget(
              inpwModel: InputWidgetModel(
                autofocus: isKeyboardVisible,
                label: AppStrings.emailText,
                hintText: AppStrings.inputEmail,
                controller: emailController,
                focusNode: emailFocus,
                onFieldSubmitted: (value) {
                  FocusScope.of(context).requestFocus(passwordFocus);
                },
                textInputAction: TextInputAction.next,
              ),
            ),

            // password
            InputWidget(
              inpwModel: InputWidgetModel(
                label: AppStrings.passwordText,
                hintText: AppStrings.inputPassword,
                onChangeTextField: (value) {
                  BlocProvider.of<LoginBloc>(context).add(EnableLoginBtn(
                    isLoginBtnEnabled: value.isNotEmpty,
                  ));
                },
                onFieldSubmitted: (value) => loginTap(),
                showSuffix: true,
                controller: passwordController,
                focusNode: passwordFocus,
              ),
            ),

            MainButton(
              isEnabled: state.isLoginBtnEnabled,
              text: AppStrings.loginText,
              onTap: loginTap,
            ),

            isKeyboardVisible
                ? Center(
                    child: Padding(
                      padding: const EdgeInsets.only(top: 24),
                      child: InkWell(
                        splashColor: Colors.white,
                        borderRadius: BorderRadius.circular(10),
                        onTap: () {
                          FocusScope.of(context).unfocus();
                          AutoRouter.of(context).push(const ForgotPasswordRoute());
                        },
                        child: Text(
                          AppStrings.forgotPassword,
                          textAlign: TextAlign.center,
                          style: AppTextStyles.textBtnTextStyle,
                        ),
                      ),
                    ),
                  )
                : const SizedBox(),
          ],
        );
      },
    );
  }
}
