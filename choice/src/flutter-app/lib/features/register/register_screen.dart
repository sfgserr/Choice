import 'package:auto_route/auto_route.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:choice/features/register/bloc/register_bloc.dart';
import 'package:choice/repositories/models/ui_models/input_widget_model.dart';
import 'package:choice/ui/ui.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'widgets/register_widgets.dart';

@RoutePage()
class RegisterScreen extends StatefulWidget {
  const RegisterScreen({
    super.key,
    required this.isCompanyRegister,
  });

  final bool isCompanyRegister;

  @override
  State<RegisterScreen> createState() => _RegisterScreenState();
}

class _RegisterScreenState extends State<RegisterScreen> {
  late TextEditingController nameController;
  late TextEditingController surnameController;
  late TextEditingController emailController;
  late TextEditingController passwordController;
  late TextEditingController confirmPasswordController;

  late FocusNode surnameFocus;
  late FocusNode emailFocus;
  late FocusNode passwordFocus;
  late FocusNode confirmPasswordFocus;

  final _formNameKey = GlobalKey<FormState>();
  final _formSurnameKey = GlobalKey<FormState>();
  final _formEmailKey = GlobalKey<FormState>();
  final _formPasswordKey = GlobalKey<FormState>();
  final _formConfirmPasswordKey = GlobalKey<FormState>();

  @override
  void initState() {
    super.initState();
    // init controllers and focusNodes
    surnameController = TextEditingController();
    surnameFocus = FocusNode();

    nameController = TextEditingController();
    emailController = TextEditingController();
    passwordController = TextEditingController();
    confirmPasswordController = TextEditingController();

    emailFocus = FocusNode();
    passwordFocus = FocusNode();
    confirmPasswordFocus = FocusNode();
  }

  @override
  void dispose() {
    nameController.dispose();
    emailController.dispose();
    emailFocus.dispose();
    passwordController.dispose();
    passwordFocus.dispose();
    confirmPasswordController.dispose();
    confirmPasswordFocus.dispose();
    surnameFocus.dispose();
    surnameController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    double alreadyLoggedInBtnTopPadding = widget.isCompanyRegister ? 120 : 22;

    return Scaffold(
      body: BlocBuilder<RegisterBloc, RegisterState>(
        builder: (context, state) {
          return CustomScrollView(
            slivers: [
              SliverAppBar(
                automaticallyImplyLeading: false,
                title: Text(
                  widget.isCompanyRegister
                      ? AppStrings.registerCompany
                      : AppStrings.registerClient,
                  style: AppTextStyles.appBarSecondTextStyle,
                ),
              ),
              SliverToBoxAdapter(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.center,
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    const SizedBox(
                      height: 10,
                    ),

                    // name
                    Form(
                      key: _formNameKey,
                      child: InputWidget(
                        inpwModel: InputWidgetModel(
                          validator: (str) => '',
                          autofocus: true,
                          label: AppStrings.nameText,
                          hintText: AppStrings.inputName,
                          controller: nameController,
                          onFieldSubmitted: (value) {
                            if (_formNameKey.currentState!.validate()) {
                              FocusScope.of(context).requestFocus(
                                widget.isCompanyRegister
                                    ? emailFocus
                                    : surnameFocus,
                              );
                            }
                          },
                          textInputAction: TextInputAction.next,
                        ),
                      ),
                    ),

                    // surname
                    if (!widget.isCompanyRegister)
                      Form(
                        key: _formSurnameKey,
                        child: InputWidget(
                          inpwModel: InputWidgetModel(
                            validator: (str) => '',
                            label: AppStrings.surnameText,
                            hintText: AppStrings.inputSurname,
                            controller: surnameController,
                            focusNode: surnameFocus,
                            onFieldSubmitted: (value) {
                              if (_formSurnameKey.currentState!.validate()) {
                                FocusScope.of(context).requestFocus(emailFocus);
                              }
                            },
                            textInputAction: TextInputAction.next,
                          ),
                        ),
                      ),

                    // email
                    Form(
                      key: _formEmailKey,
                      child: InputWidget(
                        inpwModel: InputWidgetModel(
                          validator: AppValidators.emailValidator,
                          label: AppStrings.emailText,
                          hintText: AppStrings.inputEmail,
                          controller: emailController,
                          focusNode: emailFocus,
                          onFieldSubmitted: (value) {
                            if (_formEmailKey.currentState!.validate()) {
                              FocusScope.of(context)
                                  .requestFocus(passwordFocus);
                            }
                          },
                          textInputAction: TextInputAction.next,
                        ),
                      ),
                    ),

                    // password
                    Form(
                      key: _formPasswordKey,
                      child: InputWidget(
                        inpwModel: InputWidgetModel(
                          validator: AppValidators.passwordValidator,
                          label: AppStrings.passwordText,
                          hintText: AppStrings.inputPassword,
                          onFieldSubmitted: (value) {
                            if (_formPasswordKey.currentState!.validate()) {
                              FocusScope.of(context)
                                  .requestFocus(confirmPasswordFocus);
                            }
                          },
                          onChangeTextField: (value) {
                            BlocProvider.of<RegisterBloc>(context)
                                .add(UpdateFirstPassword(newPassword: value));
                          },
                          showSuffix: true,
                          obscureText: state.isObscurePassword,
                          controller: passwordController,
                          focusNode: passwordFocus,
                          textInputAction: TextInputAction.next,
                        ),
                        onSuffixTap: () {
                          BlocProvider.of<RegisterBloc>(context)
                              .add(ObscurePasswordText());
                        },
                      ),
                    ),

                    // confirm password
                    Form(
                      key: _formConfirmPasswordKey,
                      child: InputWidget(
                        inpwModel: InputWidgetModel(
                          validator: AppValidators.confirmPasswordValidator,
                          label: AppStrings.repeatPassword,
                          hintText: AppStrings.inputPassword,
                          onChangeTextField: (value) {
                            BlocProvider.of<RegisterBloc>(context).add(
                              EnableMainBtn(isMainBtnEnabled: value.isNotEmpty),
                            );
                            BlocProvider.of<RegisterBloc>(context)
                                .add(UpdateSecondPassword(newPassword: value));
                          },
                          onFieldSubmitted: (value) {
                            if (state.isMainBtnEnabled &&
                                _formPasswordKey.currentState!.validate() &&
                                _formConfirmPasswordKey.currentState!
                                    .validate()) {
                              // action
                              FocusScope.of(context).unfocus();

                              // show
                              showCupertinoModalPopup(
                                barrierColor: Colors.black45,
                                context: context,
                                builder: (_) => const RegisterFinishedDialog(),
                              );
                            }
                          },
                          showSuffix: true,
                          obscureText: state.isObscureConfirmPassword,
                          controller: confirmPasswordController,
                          focusNode: confirmPasswordFocus,
                        ),
                        onSuffixTap: () {
                          BlocProvider.of<RegisterBloc>(context)
                              .add(ObscureConfirmPasswordText());
                        },
                      ),
                    ),

                    MainButton(
                      isEnabled: state.isMainBtnEnabled,
                      text: AppStrings.createAccountText,
                      onTap: () {},
                    ),
                  ],
                ),
              ),
              SliverPadding(
                padding: EdgeInsets.fromLTRB(
                    110, alreadyLoggedInBtnTopPadding, 110, 30),
                sliver: SliverToBoxAdapter(
                  child: Column(
                    children: [
                      Text(
                        AppStrings.alreadyLoggedIn,
                        textAlign: TextAlign.center,
                        style: AppTextStyles.logoTextStyle,
                      ),
                      MyTextButton(
                        text: AppStrings.loginText,
                        padding: const EdgeInsets.only(top: 6),
                        onTap: () => AutoRouter.of(context).pop(),
                      ),
                    ],
                  ),
                ),
              ),
            ],
          );
        },
      ),
    );
  }
}
