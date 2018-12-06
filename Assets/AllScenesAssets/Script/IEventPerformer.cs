using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;

interface IEventPerformer<T> {
    IObservable<Unit> next(T variable);
    IObservable<Unit> hide();
    IObservable<Unit> show();

}

