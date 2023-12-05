import 'package:auto_route/auto_route.dart';
import 'package:choice/config/theme/colors.dart';
import 'package:choice/domain/blocs/forgot_password_bloc/export_forgot_password_bloc.dart';
import 'package:choice/ui/components/main_button.dart';
import 'package:choice/ui/pages/login/models/input_widget_model.dart';
import 'package:choice/ui/pages/login/widgets/input_widget.dart';
import 'package:choice/ui/utils/strings.dart';
import 'package:choice/ui/utils/text_styles.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

@RoutePage()
class ForgotPasswordScreen extends StatefulWidget {
  const ForgotPasswordScreen({super.key});

  @override
  State<ForgotPasswordScreen> createState() => _ForgotPasswordScreenState();
}

class _ForgotPasswordScreenState extends State<ForgotPasswordScreen> {
  late TextEditingController emailController;

  void sendCode() {
    FocusScope.of(context).unfocus();
  }

  @override
  void initState() {
    super.initState();
    emailController = TextEditingController();
  }

  @override
  void dispose() {
    emailController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<ForgotPasswordBloc, ForgotPasswordState>(
      builder: (context, state) {
        return Scaffold(
          body: CustomScrollView(
            slivers: [
              SliverAppBar(
                pinned: true,
                centerTitle: true,
                leading: InkWell(
                  onTap: () => AutoRouter.of(context).pop(),
                  child: Icon(
                    CupertinoIcons.chevron_left,
                    size: 28,
                    color: AppColors.primaryColor,
                  ),
                ),
                title: Text(
                  AppStrings.restorePassword,
                  textAlign: TextAlign.center,
                  style: AppTextStyles.appBarTextStyle,
                ),
              ),
              SliverToBoxAdapter(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.center,
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    SizedBox(
                      height: 42,
                    ),
                    Padding(
                      padding: EdgeInsets.symmetric(horizontal: 16),
                      child: Text(
                        AppStrings.weWillSendCodeText,
                        style: AppTextStyles.bodySmallTextStyle,
                      ),
                    ),
                    // email
                    InputWidget(
                      inpwModel: InputWidgetModel(
                        label: AppStrings.emailText,
                        hintText: AppStrings.inputEmail,
                        controller: emailController,
                        onChangeTextField: (value) {
                          BlocProvider.of<ForgotPasswordBloc>(context).add(
                            EnableMainBtn(isEnabledMainBtn: value.isNotEmpty),
                          );
                        },
                        onFieldSubmitted: (value) => sendCode,
                      ),
                    ),

                    MainButton(
                      isEnabled: state.isEnabledMainBtn,
                      text: AppStrings.sendCodeText,
                      onTap: sendCode,
                    ),
                  ],
                ),
              ),
            ],
          ),
        );
      },
    );
  }
}
