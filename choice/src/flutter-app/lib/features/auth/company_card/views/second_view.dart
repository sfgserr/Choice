import 'package:choice/features/auth/company_card/bloc/card_company_bloc.dart';
import 'package:choice/features/auth/company_card/widgets/widgets.dart';
import 'package:choice/ui/ui.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';

class SecondView extends StatefulWidget {
  const SecondView({super.key});

  @override
  State<SecondView> createState() => _SecondViewState();
}

class _SecondViewState extends State<SecondView> {
  late ScrollController scrollController;

  @override
  void initState() {
    super.initState();
    scrollController = ScrollController();
  }

  @override
  void dispose() {
    scrollController.dispose();
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
          child: SelectedPageWidget(selectedIndex: 1),
        ),
        SliverToBoxAdapter(
          child: Padding(
            padding: const EdgeInsets.symmetric(horizontal: 16),
            child: Column(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Divider(
                  color: AppColors.light2Color,
                ),
                const SizedBox(height: 9),
                Align(
                  alignment: Alignment.topLeft,
                  child: Text(
                    AppStrings.socialNetworks,
                    style: AppTextStyles.bodySmallTextStyle.copyWith(
                      fontWeight: FontWeight.w700,
                      fontSize: 17,
                    ),
                  ),
                ),
                const SizedBox(height: 9),
                const Column(
                  mainAxisSize: MainAxisSize.max,
                  children: [
                    // inst
                    SocialNetworkWidget(title: 'Instagram', assetName: 'inst'),
                    SocialNetworkWidget(
                        title: 'Facebook', assetName: 'facebook'),
                    SocialNetworkWidget(title: 'ВК', assetName: 'vk'),
                    SocialNetworkWidget(title: 'Telegram', assetName: 'tg'),
                  ],
                ),
                Align(
                  alignment: Alignment.bottomCenter,
                  child: BlocBuilder<CardCompanyBloc, CardCompanyState>(
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

    // check if at least one of the social networks is selected
    if (true) {
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

class SocialNetworkWidget extends StatelessWidget {
  const SocialNetworkWidget({
    super.key,
    required this.title,
    required this.assetName,
  });

  final String title;
  final String assetName;

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisSize: MainAxisSize.min,
      children: [
        ListTile(
          dense: true,
          visualDensity: VisualDensity(vertical: -4),
          contentPadding: const EdgeInsets.symmetric(horizontal: 4),
          title: Text(title),
          titleTextStyle: AppTextStyles.bodySmallTextStyle.copyWith(
            fontSize: 15,
          ),
          leading: SvgPicture.asset(
            'assets/svg/$assetName.svg',
            width: 24,
            height: 24,
            fit: BoxFit.cover,
          ),
          trailing: CupertinoSwitch(
            value: false,
            onChanged: (val) {},
          ),
        ),
        Divider(
          color: AppColors.lightColor,
        )
      ],
    );
  }
}
