import 'package:choice/features/auth/company_card/widgets/widgets.dart';
import 'package:choice/ui/ui.dart';
import 'package:flutter/material.dart';

class ThirdView extends StatefulWidget {
  const ThirdView({super.key});

  @override
  State<ThirdView> createState() => _ThirdViewState();
}

class _ThirdViewState extends State<ThirdView> {
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
          child: SelectedPageWidget(selectedIndex: 2),
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
              ],
            ),
          ),
        ),
      ],
    );
  }
}
