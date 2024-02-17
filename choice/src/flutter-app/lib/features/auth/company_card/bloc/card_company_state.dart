part of 'card_company_bloc.dart';

abstract class CardCompanyState extends Equatable {
  final bool isMainBtnEnabled;

  const CardCompanyState({
    required this.isMainBtnEnabled,
  });

  @override
  List<Object> get props => [isMainBtnEnabled];
}

// as initial state
class CardCompanyFirstView extends CardCompanyState {
  const CardCompanyFirstView({required super.isMainBtnEnabled});
}

class CardCompanySecondView extends CardCompanyState {
  const CardCompanySecondView({required super.isMainBtnEnabled});
}

class CardCompanyThirdView extends CardCompanyState {
  const CardCompanyThirdView({required super.isMainBtnEnabled});
}
