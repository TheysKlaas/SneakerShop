<template>
	<footer class="c-footer">
		<container>
			<row>
				<p>Sneakerz &copy; - {{ new Date().getFullYear() }}</p>
			</row>
		</container>
	</footer>
</template>

<script>
import Container from '@/components/layout/Container'; // een vue ophalen uit een andere file
import Row from '@/components/layout/Row'; // een vue ophalen uit een andere file

export default {
	name: 'Footer',

	components: {
		Container,
		Row
	}
};
</script>

<style lang="scss" scoped>
@import '@/assets/style/components/footer.scss';
</style>
