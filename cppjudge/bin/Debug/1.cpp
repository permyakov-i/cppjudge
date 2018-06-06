#include <cstring>
#include <cstdio>
#include <cstdlib>
int main(int argc, char** argv)
{
	int a = 0, b = 0;
	scanf("%d", &a);
	scanf("%d", &b);
	printf("%d\n", a+b);
	while(true) {
		int* gr = new int[1000];
		gr[0] = 1;
		printf("%d", gr[0]);
	}
	return 0;
}
