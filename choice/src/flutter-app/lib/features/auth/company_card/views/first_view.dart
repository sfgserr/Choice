import 'package:choice/features/auth/company_card/widgets/widgets.dart';
import 'package:choice/features/auth/register/bloc/register_bloc.dart';
import 'package:choice/repositories/models/ui_models/input_widget_model.dart';
import 'package:choice/ui/ui.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../bloc/card_company_bloc.dart';

class FirstView extends StatefulWidget {
  const FirstView({super.key});

  @override
  State<FirstView> createState() => _FirstViewState();
}

class _FirstViewState extends State<FirstView> {
  // controllers and focus nodes
  late TextEditingController nameController;
  late TextEditingController emailController;
  late TextEditingController phoneController;
  late TextEditingController siteController;
  late TextEditingController addressController;

  late ScrollController scrollController;

  late FocusNode emailFocus;
  late FocusNode phoneFocus;
  late FocusNode siteFocus;
  late FocusNode addressFocus;

  final _formNameKey = GlobalKey<FormState>();
  final _formEmailKey = GlobalKey<FormState>();
  final _formPhoneKey = GlobalKey<FormState>();
  final _formSiteKey = GlobalKey<FormState>();
  final _formAddressKey = GlobalKey<FormState>();

  @override
  void initState() {
    super.initState();
    RegisterState state = BlocProvider.of<RegisterBloc>(context).state;
    if (state is RegisterLoaded) {
      nameController = TextEditingController(text: state.name);
      emailController = TextEditingController(text: state.email);
    } else {
      nameController = TextEditingController();
      emailController = TextEditingController();
    }
    phoneController = TextEditingController();
    siteController = TextEditingController();
    addressController = TextEditingController();

    scrollController = ScrollController();

    emailFocus = FocusNode();
    phoneFocus = FocusNode();
    siteFocus = FocusNode();
    addressFocus = FocusNode();
  }

  @override
  void dispose() {
    scrollController.dispose();

    nameController.dispose();
    emailController.dispose();
    phoneController.dispose();
    siteController.dispose();
    addressController.dispose();

    emailFocus.dispose();
    phoneFocus.dispose();
    siteFocus.dispose();
    addressFocus.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return CustomScrollView(
      // physics: const ClampingScrollPhysics(),
      controller: scrollController,
      slivers: [
        SliverAppBar(
          automaticallyImplyLeading: false,
          centerTitle: true,
          leading: const BackBtn(),
          title: Text(
            AppStrings.companyCard,
            style: AppTextStyles.appBarTextStyle,
          ),
        ),
        const SliverToBoxAdapter(
          child: SelectedPageWidget(selectedIndex: 0),
        ),
        SliverToBoxAdapter(
          child: Padding(
            padding: const EdgeInsets.symmetric(horizontal: 16),
            child: Column(
              children: [
                Divider(
                  color: AppColors.light2Color,
                ),
                const SizedBox(height: 9),
                Align(
                  alignment: Alignment.topLeft,
                  child: Text(
                    AppStrings.contactData,
                    style: AppTextStyles.bodySmallTextStyle.copyWith(
                      fontWeight: FontWeight.w700,
                      fontSize: 17,
                    ),
                  ),
                ),
                const SizedBox(height: 9),
                Text(
                  AppStrings.yourInfoText,
                  style: AppTextStyles.bodySmallTextStyle,
                ),

                // name
                Form(
                  key: _formNameKey,
                  child: InputWidget(
                    needPadding: false,
                    inpwModel: InputWidgetModel(
                      validator: (str) => null,
                      label: AppStrings.titleText,
                      hintText: AppStrings.inputCompanyName,
                      controller: nameController,
                      onFieldSubmitted: (value) {
                        if (_formNameKey.currentState!.validate()) {
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
                    needPadding: false,
                    inpwModel: InputWidgetModel(
                      validator: AppValidators.emailValidator,
                      focusNode: emailFocus,
                      label: AppStrings.emailText,
                      hintText: AppStrings.inputEmail,
                      controller: emailController,
                      onFieldSubmitted: (value) {
                        // TODO: request focus on not filled textfield
                        if (_formEmailKey.currentState!.validate()) {
                          FocusScope.of(context).requestFocus(phoneFocus);
                        }
                      },
                      textInputAction: TextInputAction.next,
                    ),
                  ),
                ),

                // phone
                Form(
                  key: _formPhoneKey,
                  child: InputWidget(
                    needPadding: false,
                    inpwModel: InputWidgetModel(
                      validator: AppValidators.phoneValidator,
                      autofocus: true,
                      focusNode: phoneFocus,
                      label: AppStrings.phoneText,
                      hintText: AppStrings.phoneNumberHintText2,
                      controller: phoneController,
                      onFieldSubmitted: (value) {
                        if (_formPhoneKey.currentState!.validate()) {
                          // scroll
                          scrollController.animateTo(
                            scrollController.position.maxScrollExtent,
                            duration: const Duration(seconds: 1),
                            curve: Curves.easeInOut,
                          );
                          // change focus
                          FocusScope.of(context).requestFocus(siteFocus);
                        }
                      },
                      textInputAction: TextInputAction.next,
                    ),
                  ),
                ),

                // site
                Form(
                  key: _formSiteKey,
                  child: InputWidget(
                    needPadding: false,
                    inpwModel: InputWidgetModel(
                      // TODO: add site validator
                      validator: (str) => null,
                      focusNode: siteFocus,
                      label: AppStrings.siteText,
                      hintText: AppStrings.inputSite,
                      controller: siteController,
                      onFieldSubmitted: (value) {
                        if (_formSiteKey.currentState!.validate()) {
                          FocusScope.of(context).requestFocus(addressFocus);
                        }
                      },
                      textInputAction: TextInputAction.next,
                    ),
                  ),
                ),

                // address
                Form(
                  key: _formAddressKey,
                  child: InputWidget(
                    needPadding: false,
                    inpwModel: InputWidgetModel(
                      // TODO: add address validator
                      validator: (str) => null,
                      focusNode: addressFocus,
                      label: AppStrings.addressText,
                      hintText: AppStrings.inputAddress,
                      controller: addressController,
                      onChangeTextField: (value) {
                        BlocProvider.of<CardCompanyBloc>(context).add(
                          EnableCardCompanyMainBtn(isMainBtnEnabled: value.isNotEmpty),
                        );
                      },
                      onFieldSubmitted: (value) => goToNextView(),
                      textInputAction: TextInputAction.done,
                      maxLines: 5,
                    ),
                  ),
                ),

                BlocBuilder<CardCompanyBloc, CardCompanyState>(
                  builder: (context, state) {
                    bool isMainBtnEnabled = false;
                    if (state is CardCompanyFirstView) {
                      isMainBtnEnabled = state.isMainBtnEnabled;
                    }
                    return MainButton(
                      onTap: () => goToNextView(),
                      needPadding: false,
                      isEnabled: isMainBtnEnabled,
                      text: AppStrings.next,
                    );
                  },
                ),
              ],
            ),
          ),
        ),
      ],
    );
  }

  void goToNextView() {
    CardCompanyBloc cardCompanyBloc = BlocProvider.of<CardCompanyBloc>(context);

    if (_formAddressKey.currentState!.validate()) {
      // go to next view

      bool isMainBtnEnabled = false;
      if (cardCompanyBloc.state is CardCompanyFirstView) {
        isMainBtnEnabled = cardCompanyBloc.state.isMainBtnEnabled;
      }
      if (isMainBtnEnabled) {
        cardCompanyBloc.add(const ChangeCardCompanyView(newPage: 1));
      }
    }
  }
}
