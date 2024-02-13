import 'package:auto_route/auto_route.dart';
import 'package:choice/features/auth/company_card/bloc/card_company_bloc.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'views/views.dart';

@RoutePage()
class CompanyCardScreen extends StatelessWidget {
  const CompanyCardScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: BlocBuilder<CardCompanyBloc, CardCompanyState>(
        builder: (context, state) {
          if (state is CardCompanyFirstView) return const FirstView();

          if (state is CardCompanySecondView) return const SecondView();

          if (state is CardCompanyThirdView) return const ThirdView();

          return const SliverToBoxAdapter();
        },
      ),
    );
  }
}
