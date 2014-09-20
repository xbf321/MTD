/*
Copyright 2011, KISSY UI Library v1.20dev
MIT Licensed
build time: Oct 18 17:37
*/
KISSY.add("base/attribute",function(i,j){function k(a,b){if(i.isString(b))return a[b];return b}function l(a,b,c){var d=a[b]||{};if(c)a[b]=d;return d}function e(a){return l(a,"__attrs",true)}function g(a){return l(a,"__attrVals",true)}function m(a,b){for(var c=0,d=b.length;a!=j&&c<d;c++)a=a[b[c]];return a}function h(){}function n(a){return a.charAt(0).toUpperCase()+a.substring(1)}h.INVALID={};var s=h.INVALID;i.augment(h,{getAttrs:function(){return e(this)},getAttrVals:function(){var a={},b,c=e(this);
for(b in c)a[b]=this.get(b);return a},addAttr:function(a,b,c){var d=e(this);b=i.clone(b);if(d[a])i.mix(d[a],b,c);else d[a]=b;return this},addAttrs:function(a,b){var c=this;i.each(a,function(f,o){c.addAttr(o,f)});if(b)for(var d in b)c.set(d,b[d]);return c},hasAttr:function(a){return a&&e(this).hasOwnProperty(a)},removeAttr:function(a){if(this.hasAttr(a)){delete e(this)[a];delete g(this)[a]}return this},set:function(a,b){var c,d,f,o=a;if(a.indexOf(".")!==-1){c=a.split(".");a=c.shift()}f=this.get(a);
if(c)d=m(f,c);if(!(!c&&f===b))if(!(c&&d===b)){if(c){var p=d=i.clone(f),r=c.length-1;if(r>=0){for(var q=0;q<r;q++)p=p[c[q]];if(p!=j)p[c[q]]=b}b=d}if(false===this.__fireAttrChange("before",a,f,b,o))return false;c=this.__set(a,b);if(c===false)return c;this.__fireAttrChange("after",a,f,g(this)[a],o);return this}},__fireAttrChange:function(a,b,c,d,f){return this.fire(a+n(b)+"Change",{attrName:b,subAttrName:f,prevVal:c,newVal:d})},__set:function(a,b){var c,d=l(e(this),a,true),f=d.validator;d=d.setter;if(f=
k(this,f))if(!f.call(this,b,a))return false;if(d=k(this,d))c=d.call(this,b,a);if(c===s)return false;if(c!==j)b=c;g(this)[a]=b},get:function(a){var b,c,d;if(a.indexOf(".")!==-1){b=a.split(".");a=b.shift()}c=l(e(this),a).getter;d=a in g(this)?g(this)[a]:this.__getDefAttrVal(a);if(c=k(this,c))d=c.call(this,d,a);if(b)d=m(d,b);return d},__getDefAttrVal:function(a){var b=l(e(this),a),c;if(c=k(this,b.valueFn)){c=c.call(this);if(c!==j)b.value=c;delete b.valueFn;e(this)[a]=b}return b.value},reset:function(a){if(a)return this.hasAttr(a)?
this.set(a,this.__getDefAttrVal(a)):this;var b=e(this);for(a in b)this.reset(a);return this}});h.__capitalFirst=n;return h});KISSY.add("base/base",function(i,j,k){function l(e){for(var g=this.constructor;g;){var m=g.ATTRS;if(m){var h=void 0;for(h in m)m.hasOwnProperty(h)&&this.addAttr(h,m[h],false)}g=g.superclass?g.superclass.constructor:null}if(e)for(var n in e)e.hasOwnProperty(n)&&this.__set(n,e[n])}i.augment(l,k.Target,j);return l},{requires:["./attribute","event"]});
KISSY.add("base",function(i,j,k){j.Attribute=k;return j},{requires:["base/base","base/attribute"]});